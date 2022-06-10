<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmUserMaster.aspx.cs" Inherits="VIEW_frmUserMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .round {
            position: relative;
        }

            .round label {
                background-color: #fff;
                border: 1px solid #ccc;
                border-radius: 50%;
                height: 16px;
                left: 10px;
                position: absolute;
                width: 16px;
            }

                .round label:after {
                    border: 2px solid #fff;
                    border-top: none;
                    border-right: none;
                    content: "";
                    height: 6px;
                    left: 2px;
                    bottom: 3px;
                    opacity: 0;
                    position: absolute;
                    top: 2px;
                    transform: rotate(-45deg);
                    width: 10px;
                }

            .round input[type="checkbox"] {
                visibility: hidden;
            }

                .round input[type="checkbox"]:checked + label {
                    background-color: #66bb6a;
                    border-color: #66bb6a;
                }

                    .round input[type="checkbox"]:checked + label:after {
                        opacity: 1;
                    }

        body {
            background-color: #f1f2f3;
            -webkit-box-align: center;
            -ms-flex-align: center;
            align-items: center;
        }

        .container {
            margin: 0 auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <asp:UpdateProgress ID="UpdateProgress" runat="server">
        <ProgressTemplate>
            <div align="center">
                <img src="../images/loading123.gif" alt="Processing" />
                 <br /><b style="font-size:small;">Please wait....</b>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
        PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcel" />
            </Triggers>
        <ContentTemplate>
            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top">
                                <span class="h_icon_he list_images"></span>
                                <h6>User Details</h6>
                                <div id="divaddbtn" runat="server" class="btn_24_blue" style="float: right;">
                                    <span class="icon add_co"></span>
                                    <asp:Button ID="btnAdd" runat="server" Text="Add New User" CssClass="btn_link"
                                        OnClick="btnAdd_Click" CausesValidation="false" />
                                </div>
                                <br />
                                <div id="divExcel" runat="server" class="btn_24_green" style="float: right;">
                                    <span class="icon excel_document"></span>
                                    <asp:Button ID="btnExcel" runat="server" Text="EXPORT EXCEL" CssClass="btn_link"
                                        OnClick="btnExcel_Click" CausesValidation="false" />
                                </div>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="InputTable" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td width="124" class="field_title">User info <span class="req">*</span>
                                            </td>
                                            <td class="field_input">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td width="200">
                                                            <asp:TextBox ID="txtempcode" runat="server" CssClass="larg" MaxLength="50"></asp:TextBox>
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtempcode"
                                                                SetFocusOnError="true" ErrorMessage="Employee Code is required!" Display="None"></asp:RequiredFieldValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender118" runat="server"
                                                                TargetControlID="RequiredFieldValidator1" PopupPosition="BottomLeft" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>--%>
                                                            <span class="label_intro">Employee Code (HR-One/Others Party)</span>
                                                        </td>
                                                        <td width="15">&nbsp;
                                                        </td>
                                                        <td width="200">
                                                            <asp:TextBox ID="txtusername" runat="server" CssClass="larg" MaxLength="50" onchange="checkUserName(this)"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="CV_UserName" runat="server" ControlToValidate="txtusername"
                                                                SetFocusOnError="true" ErrorMessage="User Name is required!" Display="None"></asp:RequiredFieldValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                                                TargetControlID="CV_UserName" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                            <span class="label_intro">User Name<span id="spnMsg"></span></span>
                                                        </td>
                                                        <td width="15">&nbsp;
                                                        </td>
                                                        <td width="200">
                                                            <div id="divtxtpassword" runat="server">
                                                                <asp:TextBox ID="txtpassword" runat="server" TextMode="Password" CssClass="larg"
                                                                    MaxLength="50"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="CV_UserPassword" runat="server" ControlToValidate="txtpassword"
                                                                    SetFocusOnError="true" ErrorMessage="Password is required!" Display="None"></asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender21" runat="server"
                                                                    TargetControlID="CV_UserPassword" PopupPosition="BottomLeft" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="../images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                                <span class="label_intro">
                                                                    <asp:Label ID="lblpassword" runat="server" Text="Password"></asp:Label></span>
                                                            </div>
                                                            <div id="divtxtpasswordedit" runat="server">
                                                                <asp:TextBox ID="txtpasswordedit" runat="server" CssClass="larg" Enabled="false"
                                                                    MaxLength="50"></asp:TextBox>
                                                                <span class="label_intro">Password</span>
                                                            </div>
                                                        </td>
                                                        <td width="15">&nbsp;
                                                        </td>
                                                        <td width="220">
                                                            <asp:TextBox ID="txtcode" runat="server" CssClass="larg" MaxLength="50" Enabled="false"></asp:TextBox>
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtcode"
                                                                SetFocusOnError="true" ErrorMessage="Employee Code is required!" Display="None"></asp:RequiredFieldValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender17" runat="server"
                                                                TargetControlID="RequiredFieldValidator5" PopupPosition="BottomLeft" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>--%>
                                                            <span class="label_intro">System Code (Auto Generated)</span>
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title">Person Details <span class="req">*</span>
                                            </td>
                                            <td class="field_input">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td width="50">
                                                            <asp:TextBox ID="txtFName" runat="server" CssClass="larg"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="CV_FNAME" runat="server" ControlToValidate="txtFName"
                                                                SetFocusOnError="true" ErrorMessage="First Name is required!" Display="None"></asp:RequiredFieldValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                                                TargetControlID="CV_FNAME" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                            <span class="label_intro">First Name</span>
                                                        </td>
                                                        <td width="5">&nbsp;
                                                        </td>
                                                        <td width="50">
                                                            <asp:TextBox ID="txtMName" CssClass="larg" runat="server"></asp:TextBox>
                                                            <span class="label_intro">Middle Name</span>
                                                        </td>
                                                        <td width="5">&nbsp;
                                                        </td>
                                                        <td width="50">
                                                            <asp:TextBox ID="txtLName" runat="server" CssClass="larg"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="CV_LNAME" runat="server" ControlToValidate="txtLName"
                                                                SetFocusOnError="true" ErrorMessage="Last Name is required!" Display="None"></asp:RequiredFieldValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                                                                TargetControlID="CV_LNAME" PopupPosition="BottomLeft" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                            <span class="label_intro">Last Name</span>
                                                        </td>
                                                        <td width="5">&nbsp;
                                                        </td>
                                                        <td width="50">
                                                            <asp:TextBox ID="txtthirdpartyname" CssClass="larg" runat="server"></asp:TextBox>
                                                            <span class="label_intro">Thirdparty Name If exist</span>
                                                        </td>
                                                        <td width="5">&nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title">Contact info <span class="req">*</span>
                                            </td>
                                            <td class="field_input">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td width="200">
                                                            <asp:TextBox ID="txtMobile" runat="server" CssClass="larg" MaxLength="10" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="CV_Mobile" runat="server" ControlToValidate="txtMobile"
                                                                SetFocusOnError="true" ErrorMessage="Mobile No is required!" Display="None"></asp:RequiredFieldValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server"
                                                                TargetControlID="CV_Mobile" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                            <asp:RegularExpressionValidator runat="server" ID="CV_Mobile_no" SetFocusOnError="true"
                                                                ControlToValidate="txtMobile" ValidationExpression="[0-9]{10}" ErrorMessage="Enter 10 digits Mobile Number!"
                                                                Display="None" />
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server"
                                                                TargetControlID="CV_Mobile_no" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                            <span class="label_intro">Mobile</span>
                                                        </td>
                                                        <td width="15">&nbsp;
                                                        </td>
                                                        <td width="200">
                                                            <asp:TextBox ID="PhoneTextBox1" runat="server" Width="200px" MaxLength="13" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                                            <asp:RegularExpressionValidator runat="server" ID="CV_TELEPHONE" SetFocusOnError="true"
                                                                ControlToValidate="PhoneTextBox1" ValidationExpression="^[\s\S]{8,}$" ErrorMessage="Minimum 8 digits PhoneNo required!"
                                                                Display="None" />
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender15" runat="server"
                                                                TargetControlID="CV_TELEPHONE" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                            <%--<asp:customvalidator ID="Customvalidator8" clientvalidationfunction="validateLength" forecolor="Red" errormessage="Telephone No is required!" controltovalidate="PhoneTextBox1" runat="server"></asp:customvalidator>--%>
                                                            <span class="label_intro">Telephone</span>
                                                        </td>
                                                        <td width="10">&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtEmail" runat="server" Width="200px"></asp:TextBox>
                                                            <%--<asp:RequiredFieldValidator ID="Cv_Email" runat="server" ControlToValidate="txtEmail"
                                                                SetFocusOnError="true" ErrorMessage="Email ID is required!" Display="None"></asp:RequiredFieldValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server"
                                                                TargetControlID="Cv_Email" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                            <asp:RegularExpressionValidator ID="CV_Email_valid" runat="server" ErrorMessage="Enter Valid Email ID!"
                                                                ControlToValidate="txtEmail" SetFocusOnError="true" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                                Display="None">
                                                            </asp:RegularExpressionValidator>--%>
                                                            <%--<asp:RegularExpressionValidator ID="CV_Email_valid" runat="server" ErrorMessage="Enter Valid Email ID!"         
                                                    ValidationExpression="^[a-z0-9](\.?[a-z0-9])([\w-\.]+@(?!gmail.com)(?!yahoo.com)(?!hotmail.com)(?!rediffmail.com)(?!outlook.com)(?!zoho.com)([\w-]+\.)+[\w-]{2,4})?$"
                                                    ControlToValidate="txtEmail" Display="None" SetFocusOnError="true"></asp:RegularExpressionValidator>--%>
                                                            <%--<ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" runat="server"
                                                                TargetControlID="CV_Email_valid" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>--%>
                                                            <span class="label_intro">Email ID</span>
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title">Address Info
                                                <span class="req">*</span>
                                            </td>
                                            <td class="field_input">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td width="415">
                                                            <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" class="input_grow"
                                                                MaxLength="255" CssClass="required large"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="CV_ADDRESS" runat="server" ControlToValidate="txtAddress"
                                                                SetFocusOnError="true" ErrorMessage="Address is required!" Display="None"></asp:RequiredFieldValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server"
                                                                TargetControlID="CV_ADDRESS" PopupPosition="BottomLeft" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                            <span class="label_intro">Contact Address</span>
                                                        </td>
                                                        <td width="15">&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtpin" runat="server" MaxLength="6" Width="100px" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="CV_PIN" runat="server" ControlToValidate="txtpin"
                                                                SetFocusOnError="true" ErrorMessage="PIN Code is required!" Display="None"></asp:RequiredFieldValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server"
                                                                TargetControlID="CV_PIN" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                            <asp:RegularExpressionValidator runat="server" ID="CV_PIN_6" SetFocusOnError="true"
                                                                ControlToValidate="txtpin" ValidationExpression="^[0-9]{6}$" ErrorMessage="Enter 6 digit PIN Code!"
                                                                Display="None" />
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server"
                                                                TargetControlID="CV_PIN_6" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                            <span class="label_intro">Pin Code</span>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title">Gender
                                                <span class="req">*</span>
                                            </td>
                                            <td class="field_input">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td width="300">
                                                            <asp:RadioButtonList ID="rdoGender" runat="server" RepeatDirection="Horizontal" Width="270">
                                                                <asp:ListItem Value="M">Male</asp:ListItem>
                                                                <asp:ListItem Value="F">Female</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                            <asp:RequiredFieldValidator runat="server" ID="radRfv" ControlToValidate="rdoGender"
                                                                SetFocusOnError="true" ErrorMessage="Gender is required!" Display="None">*</asp:RequiredFieldValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server"
                                                                TargetControlID="radRfv" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>

                                                        </td>
                                                        <td width="50" style="font-weight: bold;">DOB
                                                        </td>
                                                        <td width="140">
                                                            <asp:TextBox ID="txtDOB" runat="server" MaxLength="10" Width="90" Enabled="false"
                                                                placeholder="dd/MM/yyyy" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="Search"
                                                                Font-Bold="true"></asp:TextBox>
                                                            <asp:ImageButton ID="imgPopup1" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarFromDate" PopupButtonID="imgPopup1" runat="server"
                                                                TargetControlID="txtDOB" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>
                                                        <td width="60" style="font-weight: bold;">ANV.DATE
                                                        </td>
                                                        <td width="140">
                                                            <asp:TextBox ID="txtAnvDate" runat="server" MaxLength="10" Width="90" Enabled="false"
                                                                placeholder="dd/mm/yyyy" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="Search"
                                                                Font-Bold="true"></asp:TextBox>
                                                            <asp:ImageButton ID="imgPopuptodate" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtenderToDate" PopupButtonID="imgPopuptodate"
                                                                runat="server" TargetControlID="txtAnvDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title">Applicable To
                                                <span class="req">*</span>
                                            </td>
                                            <td class="field_input">
                                                <asp:RadioButtonList ID="rdbapplicableto" runat="server" RepeatDirection="Horizontal"
                                                    Width="40%" OnSelectedIndexChanged="rdbapplicableto_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="C">Company</asp:ListItem>
                                                    <asp:ListItem Value="D">Distribution Channel</asp:ListItem>
                                                    <asp:ListItem Value="T">Third Party</asp:ListItem>
                                                </asp:RadioButtonList>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="rdbapplicableto"
                                                    SetFocusOnError="true" ErrorMessage="ApplicableTo is required!" Display="None">*</asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender16" runat="server"
                                                    TargetControlID="RequiredFieldValidator3" PopupPosition="Right" HighlightCssClass="errormassage"
                                                    WarningIconImageUrl="../images/050.png">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title">Department
                                                <span class="req">*</span>
                                            </td>
                                            <td class="field_input">
                                                <asp:DropDownList ID="ddldept" runat="server" class="chosen-select" Width="250">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="CV_DEPARTMENT" runat="server" ControlToValidate="ddldept"
                                                    InitialValue="0" SetFocusOnError="true" ErrorMessage="*"
                                                    Display="None"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender13" runat="server"
                                                    TargetControlID="CV_DEPARTMENT" PopupPosition="Right" HighlightCssClass="errormassage"
                                                    WarningIconImageUrl="../images/050.png">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title">User Role
                                                <span class="req">*</span>
                                            </td>
                                            <td class="field_input">
                                                <asp:DropDownList ID="ddlUserType" runat="server" AutoPostBack="true" class="chosen-select"
                                                    data-placeholder="Select" Width="250" OnSelectedIndexChanged="ddlUserType_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="CV_USERTYPE" runat="server" ControlToValidate="ddlUserType"
                                                    InitialValue="0" SetFocusOnError="true" ErrorMessage="User Role is required!"
                                                    Display="None"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server"
                                                    TargetControlID="CV_USERTYPE" PopupPosition="Right" HighlightCssClass="errormassage"
                                                    WarningIconImageUrl="../images/050.png">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <%--<tr id="divTsiType" runat="server" style="display: none;"> --%>     <%--TSI TYPE NEW ADDED ON 19022019--%>
                                        <%--<td class="field_title">Tsi Type
                                            </td>
                                            <td> 
                                                <asp:DropDownList ID="ddlTsiType" Width="250" runat="server" class="chosen-select"
                                                        data-placeholder="Choose Tsi Type" AppendDataBoundItems="True" ValidationGroup="Save" AutoPostBack="true">
                                                        <asp:ListItem Text="Non Red Champ" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Red Champ" Value="2"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                        </tr>--%>
                                        <tr id="divddl" runat="server" style="display: none;">
                                            <td class="field_title"></td>
                                            <td>
                                                <div style="overflow: scroll; height: 250px; width: 40%;">
                                                    <asp:GridView ID="gvPTPUMap" runat="server" Width="100%" CssClass="reportgrid"
                                                        AutoGenerateColumns="false" ShowFooter="false" AlternatingRowStyle-BackColor="AliceBlue"
                                                        EmptyDataText="No Records Available" OnRowDataBound="gvPTPUMap_RowDataBound">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="chkAllSelect" runat="server" onclick="CheckAllheadertpu(this);" Text=" " />
                                                                </HeaderTemplate>
                                                                <HeaderStyle Width="25px" />
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="ChkIDTPU" runat="server" Text=" " ToolTip='<%# Bind("VENDORID") %>' onclick="setrowcolor(this);" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblVENDORNAME" runat="server" Text='<%# Bind("VENDORNAME") %>'
                                                                        value='<%# Eval("VENDORNAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="VENDORID" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblVENDORID" runat="server" Text='<%# Bind("VENDORID") %>'
                                                                        value='<%# Eval("VENDORID") %>' Visible="false"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title">Reporitng (Role)
                                                <span class="req">*</span>
                                            </td>
                                            <td class="field_input">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td width="300">
                                                            <asp:DropDownList ID="ddlReportToRole" runat="server" class="chosen-select" data-placeholder="Select"
                                                                Width="250" OnSelectedIndexChanged="ddlReportToRole_SelectedIndexChanged" AutoPostBack="true">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlReportToRole"
                                                                InitialValue="0" SetFocusOnError="true" ErrorMessage="*" Display="None"></asp:RequiredFieldValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtenderReportToRole" runat="server"
                                                                TargetControlID="RequiredFieldValidator4" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                        </td>
                                                        <td style="font-weight: bold;" width="120">REPORITNG (PERSON)
                                                        <span class="req">*</span>
                                                        </td>
                                                        <td width="300">
                                                            <asp:DropDownList ID="ddlReportingTo" runat="server" class="chosen-select" data-placeholder="Select"
                                                                OnSelectedIndexChanged="ddlReportingTo_SelectedIndexChanged" AutoPostBack="true" Width="250">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlReportingTo"
                                                                InitialValue="0" SetFocusOnError="true" ErrorMessage="ReporitngTo is required!"
                                                                Display="None"></asp:RequiredFieldValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server"
                                                                TargetControlID="RequiredFieldValidator2" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title">User Status
                                                <span class="req">*</span>
                                            </td>
                                            <td class="field_input">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td width="300">
                                                            <div class="select-style-inner" style="width: 100px;">
                                                                <asp:DropDownList ID="ddlUserStatus" runat="server">
                                                                    <asp:ListItem Value="1" Selected="True">Active</asp:ListItem>
                                                                    <asp:ListItem Value="2">Inactive</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </td>
                                                        <td style="font-weight: bold;" width="120">HEAD QUATER
                                                        <span class="req">*</span>
                                                        </td>
                                                        <td width="300">
                                                            <asp:DropDownList ID="ddlheadquater" runat="server" class="chosen-select" data-placeholder="Select"
                                                                Width="250">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfv_ddlheadquater" runat="server" ControlToValidate="ddlheadquater"
                                                                InitialValue="0" SetFocusOnError="true" ErrorMessage="Required!" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;
                                            </td>
                                            <td style="padding: 8px 0;">
                                                <div class="btn_24_blue">
                                                    <span class="icon disk_co"></span>
                                                    <asp:Button ID="btnsave" runat="server" Text="Save " OnClick="btnsave_Click" CssClass="btn_link" />
                                                </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_blue">
                                                    <span class="icon cross_octagon_co"></span>
                                                    <asp:Button ID="btnclose" runat="server" Text="Cancel" OnClick="btnclose_Click" CssClass="btn_link"
                                                        CausesValidation="false" />
                                                </div>
                                                <asp:HiddenField ID="Hdn_Fld" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlBeatMap" runat="server">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="form_container_td">
                                        <tr>
                                            <td class="field_title">
                                                <fieldset>
                                                    <legend>Storelocation Mapping</legend>
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="form_container_td">
                                                        <tr>
                                                            <td class="field_title" width="60">USER
                                                            </td>
                                                            <td class="field_input" width="120">
                                                                <asp:TextBox ID="txtUsernameMap" runat="server" Enabled="false" Width="250"></asp:TextBox>
                                                            </td>
                                                            <td class="field_title" width="60">STORE LOCATION<span class="req">*</span>
                                                            </td>
                                                            <td class="field_input" width="220">
                                                                <asp:DropDownList ID="ddlStoreLocation" runat="server" AppendDataBoundItems="true" 
                                                                    class="chosen-select" Width="200">
                                                                </asp:DropDownList>
                                                            </td>

                                                             <td class="field_title" style="width:auto" >Inactive User Storeloaction Mapping<span class="req">*</span>
                                                            </td>
                                                            <td class="field_input">
                                                                <div class="btn_24_red">
                                                                <asp:Button ID="btnInactiveStoreLocation" runat="server" Text="Inactive" 
                                                                    CausesValidation="false" OnClick="btnInactiveStoreLocation_Click" CssClass="btn_link" />
                                                                    </div>
                                                            </td>
                                                            <td class="field_title" style="display:none" >MENU<span class="req">*</span>
                                                            </td>
                                                            <td class="field_input" style="display:none">
                                                                <asp:DropDownList ID="ddlMenuId" runat="server" AppendDataBoundItems="true" 
                                                                    class="chosen-select" Width="200">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td class="field_title" width="60" style="display: none;">DISTRICT
                                                            </td>
                                                            <td class="field_input" width="250" style="display: none;">
                                                                <asp:DropDownList ID="ddlDistrict" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                                                    class="chosen-select" Width="200" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"
                                                                    ValidateEmptyText="false">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr style="display:none">
                                                            <td>&nbsp;
                                                            </td>
                                                            <td colspan="5">
                                                                <div>
                                                                    <table width="80%" style="border-color: White; border-width: medium; background-color: #828c95; color: White; height: 25px;">
                                                                        <tr>
                                                                            <td width="30%" align="center">SL
                                                                            </td>
                                                                            <td width="30%" align="center">USERNAME
                                                                            </td>
                                                                            <td width="30%" align="center">STORELOCATION
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                                <div id="Div1" style="overflow: scroll; height: 300px; width: 80%;">
                                                                    <asp:GridView ID="gvBeatMap" CssClass="reportgrid" runat="server" AutoGenerateColumns="False"
                                                                        Width="100%" ShowHeader="false" EmptyDataText="No Records Available" AlternatingRowStyle-BackColor="AliceBlue">
                                                                        <RowStyle Height="22px" />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="10">
                                                                                <ItemTemplate>
                                                                                    <nav style="position: relative; text-align: left;"><span class="badge black"><%# Container.DataItemIndex + 1 %></span></nav>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField HeaderText="USERNAME" DataField="USERNAME" ItemStyle-Width="30%" />
                                                                            <asp:TemplateField HeaderText="STORENAME" ItemStyle-Width="30%">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblStoreName" runat="server" Text='<%# Bind("STORENAME") %>' value='<%# Eval("STORENAME") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="STOREID" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblStoreId" runat="server" Text='<%# Bind("STOREID") %>' value='<%# Eval("STOREID") %>'
                                                                                        Visible="false"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField HeaderText="" DataField="USERID" ItemStyle-Width="30%"  Visible="false"/>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="field_title">&nbsp;</td>
                                                            <td class="field_input" style="padding: 8px 0;" colspan="5">
                                                                <div class="btn_24_blue">
                                                                    <span class="icon disk_co"></span>
                                                                    <asp:Button ID="btnBeatMapSubmit" runat="server" Text="Save" ValidationGroup="beat"
                                                                        CssClass="btn_link" OnClick="btnBeatMapSubmit_Click" />
                                                                </div>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                                <div class="btn_24_blue">
                                                                    <span class="icon cross_octagon_co"></span>
                                                                    <asp:Button ID="btnBeatMapCancel" runat="server" Text="Cancel" CausesValidation="false"
                                                                        CssClass="btn_link" OnClick="btnBeatMapCancel_Click" />
                                                                </div>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                                <asp:Label ID="lbltotalbeatcount" runat="server" Text="" Font-Bold="true"
                                                                    Font-Size="Small"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlBrand" runat="server">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="form_container_td">
                                        <tr>
                                            <td class="field_title" width="60">USER
                                            </td>
                                            <td class="field_input">
                                                <asp:TextBox ID="txtBrandusername" runat="server" Enabled="false" Width="30%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" class="field_input" style="padding-left: 10px;">
                                                <fieldset>
                                                    <legend>Item Type Mapping</legend>
                                                    <div class="gridcontent" style="overflow: scroll; height: 300px; width: 40%;">
                                                        <asp:GridView ID="grdbrand" runat="server" Width="100%" CssClass="reportgrid"
                                                            AutoGenerateColumns="false" ShowHeader="true" EmptyDataText="No Records Available"
                                                            AlternatingRowStyle-BackColor="AliceBlue">
                                                            <RowStyle Height="22px" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5px">
                                                                    <ItemTemplate>
                                                                        <nav><span class="badge black"><%# Container.DataItemIndex + 1 %></span></nav>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="DIVID" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDIVID" runat="server" Text='<%# Bind("DIVID") %>' value='<%# Eval("DIVID") %>'
                                                                            Visible="false"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="BRAND" DataField="DIVNAME" ItemStyle-Width="80%" />
                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                                    <HeaderStyle Width="30px" />
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="ChkID" runat="server" Text=" " class="round" Style="float: left; padding-right: 10px;" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </fieldset>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title"></td>
                                            <td class="field_input">
                                                <div class="btn_24_blue">
                                                    <span class="icon disk_co"></span>
                                                    <asp:Button ID="btnbrandSubmit" runat="server" Text="Submit" CausesValidation="false"
                                                        CssClass="btn_link" OnClick="btnbrandSubmit_Click" />
                                                </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_blue">
                                                    <span class="icon cross_octagon_co"></span>
                                                    <asp:Button ID="btnbrandCancel" runat="server" Text="Cancel" CausesValidation="false"
                                                        CssClass="btn_link" OnClick="btnbrandCancel_Click" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="plnMenuMapping" runat="server">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="form_container_td">
                                        <tr>
                                            <td class="field_title">
                                                <fieldset>
                                                    <legend>Exception ReportingTo Mapping</legend>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                                        <tr style="display: none;">
                                                            <td width="130" class="field_title">ROLE
                                                            </td>
                                                            <td width="200" class="field_input" colspan="6">
                                                                <asp:TextBox ID="txtuserrolemap" runat="server" CssClass="full" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="130" class="field_title">USER
                                                            </td>
                                                            <td width="100" class="field_input">
                                                                <asp:TextBox ID="txtusernamemapping" runat="server" CssClass="full" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td width="100" class="field_title">Page Menu<span class="req">*</span>
                                                            </td>
                                                            <td width="200" class="field_input">
                                                                <asp:DropDownList ID="ddlmenu" runat="server" Width="200px" class="chosen-select"
                                                                    data-placeholder="Choose Business" AppendDataBoundItems="true" ValidationGroup="ROLE">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfv_ddlmenu" runat="server" ErrorMessage="Required"
                                                                    ControlToValidate="ddlmenu" ValidateEmptyText="false" SetFocusOnError="true"
                                                                    InitialValue="0" ForeColor="Red" ValidationGroup="ROLE"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td width="80" class="field_title">EFFECTIVE PERIOD<span
                                                                class="req">*</span>
                                                            </td>
                                                            <td width="120" class="field_input">
                                                                <asp:TextBox ID="txtfromdate" runat="server" Width="70" Enabled="false" placeholder="dd/mm/yyyy"
                                                                    MaxLength="10" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="ADD"></asp:TextBox>
                                                                <asp:ImageButton ID="imgPopup" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                    runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender4" PopupButtonID="imgPopup" runat="server"
                                                                    TargetControlID="txtfromdate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                                <span class="label_intro">
                                                                    <asp:Label ID="Label26" runat="server" Text="FROM DATE"></asp:Label>
                                                                    <span class="req">*</span></span>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txttodate" runat="server" Width="70" Enabled="false" placeholder="dd/mm/yyyy"
                                                                    MaxLength="10" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="ADD"></asp:TextBox>
                                                                <asp:ImageButton ID="imgpopupto" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                    runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender5" PopupButtonID="imgpopupto" runat="server"
                                                                    TargetControlID="txttodate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                                <span class="label_intro">
                                                                    <asp:Label ID="Label27" runat="server" Text="TO DATE"></asp:Label>
                                                                    <span class="req">*</span></span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="130" class="field_title">Reporitng (Role)
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td width="100" class="field_input">
                                                                <asp:DropDownList ID="ddlreportingrolemap" runat="server" class="chosen-select" data-placeholder="Select"
                                                                    Width="210px" OnSelectedIndexChanged="ddlreportingrolemap_SelectedIndexChanged"
                                                                    AutoPostBack="true" ValidationGroup="ROLE">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlreportingrolemap"
                                                                    InitialValue="0" SetFocusOnError="true" ErrorMessage="Required!" ForeColor="Red"
                                                                    ValidationGroup="ROLE"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td width="100" class="field_title">Reporitng To
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td width="200" class="field_input">
                                                                <asp:DropDownList ID="ddlreportingtomap" runat="server" class="chosen-select" data-placeholder="Select"
                                                                    Width="200px" ValidationGroup="ROLE" AppendDataBoundItems="true">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RFV_ddlreportingtomap" runat="server" ErrorMessage="Required!"
                                                                    ControlToValidate="ddlreportingtomap" ValidateEmptyText="false" SetFocusOnError="true"
                                                                    InitialValue="0" ForeColor="Red" ValidationGroup="ROLE"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td class="field_input" colspan="3">
                                                                <div class="btn_24_blue">
                                                                    <span class="icon add_co"></span>
                                                                    <asp:Button ID="btnAddmenu" runat="server" Text="ADD" CssClass="btn_link" OnClick="btnAddmenu_Click"
                                                                        ValidationGroup="ROLE" />
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="field_title">&nbsp;
                                                            </td>
                                                            <td class="field_input" colspan="6">
                                                                <div class="gridcontent">
                                                                    <asp:GridView ID="grdAddmenu" EmptyDataText="There are no records available." AllowPaging="true" ShowFooter="false"
                                                                        CssClass="reportgrid" runat="server" AutoGenerateColumns="False" Width="100%">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <nav style="position: relative; text-align: center;"><span class="badge black"><%# Container.DataItemIndex + 1 %></span></nav>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="GUID" ItemStyle-HorizontalAlign="Left" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblGUID" runat="server" Text='<%# Eval("GUID") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="MENUNAME" HeaderText="MENU NAME" />
                                                                            <asp:BoundField DataField="REPOTINGROLENAME" HeaderText="REPOTING ROLE NAME" />
                                                                            <asp:BoundField DataField="REPORTINGTONAME" HeaderText="REPORTING TO NAME" />
                                                                            <asp:TemplateField ItemStyle-Width="5" HeaderText="DEL" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:Button ID="btngriddelete" runat="server" Text="Delete" OnClick="btngriddelete_Click" 
                                                                                        class="action-icons c-delete" CausesValidation="false" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="field_title">&nbsp;
                                                            </td>
                                                            <td style="padding-left: 10px;" colspan="6" class="field_input">
                                                                <div class="btn_24_blue">
                                                                    <span class="icon disk_co"></span>
                                                                    <asp:Button ID="btnusermappingsave" runat="server" Text="Save" CssClass="btn_link"
                                                                        OnClick="btnusermappingsave_Click" ValidationGroup="ADD" />
                                                                </div>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                                <div class="btn_24_blue">
                                                                    <span class="icon cross_octagon_co"></span>
                                                                    <asp:Button ID="btnusermappingcancel" runat="server" Text="Cancel" CssClass="btn_link"
                                                                        CausesValidation="false" OnClick="btnusermappingcancel_Click" />
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlDepotMapping" runat="server">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                        <tr>
                                            <td class="field_title" width="60">USER
                                            </td>
                                            <td class="field_input">
                                                <asp:TextBox ID="txtCustomername" runat="server" Width="30%" Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" class="field_input" style="padding-left: 10px;">
                                                <fieldset>
                                                    <legend>DEPOT/ OFFICE/ FACTORY MAPPING</legend>
                                                    <div class="gridcontent" style="overflow: scroll; height: 300px; width: 40%;">
                                                        <asp:GridView ID="gvDepotMapping" runat="server" Width="100%" CssClass="reportgrid"
                                                            AutoGenerateColumns="false" ShowHeader="true" EmptyDataText="No Records Available"
                                                            AlternatingRowStyle-BackColor="AliceBlue">
                                                            <RowStyle Height="22px" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5px">
                                                                    <ItemTemplate>
                                                                        <nav><span class="badge black"><%# Container.DataItemIndex + 1 %></span></nav>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="BRID" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblBRID" runat="server" Text='<%# Bind("BRID") %>' value='<%# Eval("BRID") %>'
                                                                            Visible="false"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="DEPOT/ OFFICE/ FACTORY" DataField="BRNAME" ItemStyle-Width="80%" />
                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                                    <HeaderStyle Width="30px" />
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="ChkID" runat="server" Text=" " class="round" Style="float: left; padding-right: 10px;" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </fieldset>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="padding: 8px 10px;">
                                                <div class="btn_24_blue">
                                                    <span class="icon disk_co"></span>
                                                    <asp:Button ID="btnDepotMapSubmit" runat="server" Text="Save" CssClass="btn_link"
                                                        CausesValidation="false" OnClick="btnDepotMapSubmit_Click" />
                                                </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_blue">
                                                    <span class="icon cross_octagon_co"></span>
                                                    <asp:Button ID="btnDepotMapCancel" runat="server" Text="Cancel" CssClass="btn_link"
                                                        CausesValidation="false" OnClick="btnDepotMapCancel_Click" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlBSmap" runat="server">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                        <tr>
                                            <td class="field_title" width="60">USER
                                            </td>
                                            <td class="field_input">
                                                <asp:TextBox ID="txtusernamebsmap" runat="server" Width="30%" Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" class="field_input" style="padding-left: 10px;">
                                                <fieldset>
                                                    <legend>BUSINEES SEGMENT MAPPING</legend>
                                                    <div class="gridcontent" style="overflow: scroll; height: 300px; width: 35%;">
                                                        <asp:GridView ID="gvbsmapping" runat="server" Width="100%" CssClass="reportgrid"
                                                            AutoGenerateColumns="false" ShowHeader="true" EmptyDataText="No Records Available">
                                                            <RowStyle Height="22px" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5px">
                                                                    <ItemTemplate>
                                                                        <nav><span class="badge black"><%# Container.DataItemIndex + 1 %></span></nav>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="BSID" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblBSID" runat="server" Text='<%# Bind("BSID") %>' value='<%# Eval("BSID") %>'
                                                                            Visible="false"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="BUSINESS SEGMENT" DataField="BSNAME" ItemStyle-Width="80%" />
                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                                    <HeaderStyle Width="30px" />
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="ChkID" runat="server" Text=" " class="round" Style="float: left; padding-right: 10px;" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </fieldset>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="padding: 8px 10px;">
                                                <div class="btn_24_blue">
                                                    <span class="icon disk_co"></span>
                                                    <asp:Button ID="btnBSMapSubmit" runat="server" Text="Save" CssClass="btn_link" CausesValidation="false"
                                                        OnClick="btnBSMapSubmit_Click" />
                                                </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_blue">
                                                    <span class="icon cross_octagon_co"></span>
                                                    <asp:Button ID="btnBSMapCancel" runat="server" Text="Cancel" CssClass="btn_link"
                                                        CausesValidation="false" OnClick="btnBSMapCancel_Click" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlbrandMAPPING" runat="server">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                        <tr>
                                            <td class="field_title" width="60">USER
                                            </td>
                                            <td class="field_input">
                                                <asp:TextBox ID="txtusernamebrandmap" runat="server" Width="30%" Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" class="field_input" style="padding-left: 1px;">
                                                <fieldset>
                                                    <legend>BRAND MAPPING</legend>
                                                    <div class="gridcontent" style="overflow: scroll; height: 300px; width: 35%;">
                                                        <asp:GridView ID="gvbrandmapping" runat="server" Width="100%" CssClass="reportgrid"
                                                            AutoGenerateColumns="false" ShowHeader="true" EmptyDataText="No Records Available"
                                                            AlternatingRowStyle-BackColor="AliceBlue">
                                                            <RowStyle Height="22px" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5px">
                                                                    <ItemTemplate>
                                                                        <nav><span class="badge black"><%# Container.DataItemIndex + 1 %></span></nav>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="DIVID" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDIVID" runat="server" Text='<%# Bind("DIVID") %>' value='<%# Eval("DIVID") %>'
                                                                            Visible="false"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="BRAND" DataField="DIVNAME" ItemStyle-Width="80%" />
                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                                    <HeaderStyle Width="30px" />
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="ChkID" runat="server" Text=" " class="round" Style="float: left; padding-right: 10px;" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </fieldset>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="padding: 8px 10px;">
                                                <div class="btn_24_blue">
                                                    <span class="icon disk_co"></span>
                                                    <asp:Button ID="btnbrandmapSubmit" runat="server" Text="Save" CssClass="btn_link"
                                                        CausesValidation="false" OnClick="btnbrandmapSubmit_Click" />
                                                </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_blue">
                                                    <span class="icon cross_octagon_co"></span>
                                                    <asp:Button ID="btnbrandmapCancel" runat="server" Text="Cancel" CssClass="btn_link"
                                                        CausesValidation="false" OnClick="btnbrandmapCancel_Click" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnldistributor" runat="server">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                        <tr>
                                            <td class="field_title" width="50">NAME<span class="req">*</span>
                                            </td>
                                            <td class="field_input" width="310">
                                                <asp:TextBox ID="txtuserdistributor" runat="server" Width="300" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td class="field_title" width="50">Depot<span class="req">*</span>
                                            </td>
                                            <td class="field_input" width="400">
                                                <asp:DropDownList ID="ddldepot" runat="server" class="chosen-select" Width="340"
                                                    OnSelectedIndexChanged="ddldepot_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5">
                                                <fieldset>
                                                    <legend>Distributor MAPPING</legend>
                                                    <div>
                                                        <table width="90%" style="border-color: White; border-width: thick; background-color: #828c95; color: White; height: 25px;">
                                                            <tr>
                                                                <td width="4%" align="center">SL
                                                                </td>
                                                                <td width="30%" align="center">CUSTOMER NAME
                                                                </td>
                                                                <td width="15%" align="center">TYPE
                                                                </td>
                                                                <td width="30%" align="center">TSI MAPPED
                                                                </td>
                                                                <td width="20%" align="center">SO MAPPED
                                                                </td>
                                                                <td width="8%" align="center"></td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div id="Div2" style="overflow: scroll; height: 300px; width: 90%;">
                                                        <asp:GridView ID="gvdistributor" runat="server" Width="100%" CssClass="reportgrid"
                                                            AutoGenerateColumns="false" ShowHeader="false" EmptyDataText="No Records Available" RowStyle-Font-Bold="true"
                                                            AlternatingRowStyle-BackColor="AliceBlue">
                                                            <RowStyle Height="22px" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="4%">
                                                                    <ItemTemplate>
                                                                        <nav><span class="badge black"><%# Container.DataItemIndex + 1 %></span></nav>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="CUSTOMERID" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCUSTOMERID" runat="server" Text='<%# Bind("CUSTOMERID") %>' value='<%# Eval("CUSTOMERID") %>'
                                                                            Visible="false"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="CUSTOMER NAME" DataField="CUSTOMERNAME" ItemStyle-Width="25%" />
                                                                <asp:BoundField HeaderText="TYPE" DataField="CUSTOMERTYPE" ItemStyle-Width="30%" />
                                                                <asp:BoundField HeaderText="TSI MAPPED" DataField="TSIMAPPED" ItemStyle-Width="30%" />
                                                                <asp:BoundField HeaderText="SO MAPPED" DataField="SOMAPPED" ItemStyle-Width="15%" />
                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                                    <HeaderTemplate>
                                                                        <%--<asp:CheckBox ID="chkAllSelect" runat="server" onclick="CheckAllheader(this);" Text=" " />--%>
                                                                    </HeaderTemplate>
                                                                    <HeaderStyle Width="8%" />
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkSelect" runat="server" Text=" " onchange="totaldistributormapped(this);" class="round" Style="float: left; padding-right: 25px;"
                                                                            onclick="totaldistributormapped(this);" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </fieldset>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5" style="padding: 8px 10px;">
                                                <div class="btn_24_blue">
                                                    <span class="icon disk_co"></span>
                                                    <asp:Button ID="btnDistributorSubmit" runat="server" Text="Save" CssClass="btn_link"
                                                        CausesValidation="false" OnClick="btnDistributorSubmit_Click" />
                                                </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_blue">
                                                    <span class="icon cross_octagon_co"></span>
                                                    <asp:Button ID="btnDistributorCancel" runat="server" Text="Cancel" CssClass="btn_link"
                                                        CausesValidation="false" OnClick="btnDistributorCancel_Click" />
                                                </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Label ID="lbltotaldistributorcount" runat="server" Text="" Font-Bold="true" Font-Size="Small"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlDisplay" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td width="60" class="field_title"><span class="slideBtn fa fa-search"></span>SEARCH</td>
                                            <td width="360" class="field_input">
                                                <asp:TextBox ID="txtnamesearch" runat="server" placeholder="Search name if any......." Width="350" Style="border-radius: 5px;"></asp:TextBox>
                                            </td>
                                            <td width="20" class="field_title">
                                                <nav style="position: relative; text-align: center;"><span class="badge green">OR</span></nav>
                                            </td>
                                            <td width="50" class="field_title">TYPE</td>
                                            <td width="80" class="field_input">
                                                <asp:DropDownList ID="ddlusertypesearch" runat="server" class="chosen-select" data-placeholder="Select"
                                                    Width="70px" AppendDataBoundItems="true">
                                                </asp:DropDownList>
                                            </td>
                                            <td width="20" class="field_title">
                                                <nav style="position: relative; text-align: center;"><span class="badge green">OR</span></nav>
                                            </td>
                                            <td width="50" class="field_title">Depot</td>
                                            <td width="150" class="field_input">
                                                <asp:DropDownList ID="ddldepotsearch" runat="server" class="chosen-select" data-placeholder="Select"
                                                    Width="140" AppendDataBoundItems="true">
                                                </asp:DropDownList>
                                            </td>
                                            <td width="150" class="field_input">
                                                <div class="btn_24_blue">
                                                    <span class="icon find_co"></span>
                                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn_link" ValidationGroup="Search"
                                                        OnClick="btnSearch_Click" />
                                                </div>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </table>

                                    <div style="overflow: hidden; padding-left: 8px; width: 100%;" id="DivHeaderRow_voucher">
                                    </div>
                                    <div style="overflow: scroll; padding-left: 8px; border: 1px solid #ccc;" onscroll="OnScrollDiv_voucher(this)" id="DivMainContent_voucher">
                                        <asp:GridView ID="gvUser" EmptyDataText="There are no records available." AllowPaging="true" OnPageIndexChanging="OnPageIndexChanging" PageSize="100" ShowFooter="true"
                                            CssClass="reportgrid" runat="server" AutoGenerateColumns="False" Width="100%" AlternatingRowStyle-BackColor="AliceBlue" Font-Bold="true">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <nav style="position: relative; text-align: center;"><span class="badge black"><%# Container.DataItemIndex + 1 %></span></nav>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="USERID" ItemStyle-HorizontalAlign="Left" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUserID" runat="server" Text='<%# Eval("USERID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CODE" HeaderText="CODE" />
                                                <asp:TemplateField HeaderText="FULL NAME">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblName" runat="server" Text='<%# Eval("NAME") %>'
                                                            ToolTip='<%# string.Format("ADDRESS : {0}\nMOBILE : {1}\nEMAIL : {2}\n", Eval("ADDRESS"), Eval("MOBILE"), Eval("EMAIL"))%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="USERNAME" HeaderText="USERNAME" />
                                                <asp:BoundField DataField="PASSWORD" HeaderText="PASSWORD" />
                                                <asp:BoundField DataField="UTNAME" HeaderText="TYPE" />
                                                <asp:BoundField DataField="REPORTINGTO" HeaderText="REPORTING TO" ItemStyle-Wrap="true" />
                                                <asp:BoundField HeaderText="HQ NAME" DataField="HQNAME" ItemStyle-Wrap="true" />
                                                <asp:TemplateField HeaderText="STATUS" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("ACTIVESTATUS") %>'
                                                            ForeColor='<%# ((string)Eval("ACTIVESTATUS")).ToLower().Equals("active") ? System.Drawing.Color.Green : System.Drawing.Color.Red %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="5" HeaderText="EDIT" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btngrdedit" runat="server" Text="Edit" OnClick="btngrdedit_Click" class="action-icons c-edit" CausesValidation="false" ToolTip='<%# Eval("NAME") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="5" HeaderText="DEL" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btngrddelete" runat="server" Text="Delete" OnClick="btngrddelete_Click" ToolTip='<%# Eval("NAME") %>'
                                                            class="action-icons c-delete" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete this user?');" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="5" HeaderText="&nbsp;CUSTOMER&nbsp; MAPPING" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btndistributorMapping" runat="server" OnClick="btndistributorMapping_Click" class="h_icon monitor" CausesValidation="false" ToolTip='<%# Eval("NAME") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="5" HeaderText="STORELOCATION &nbsp;MAPPING&nbsp;" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnbeatMapping" runat="server" OnClick="btnbeatMapping_Click" class="h_icon address_sl" CausesValidation="false" ToolTip='<%# Eval("NAME") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="5" HeaderText="DEPOT &nbsp;MAPPING&nbsp;" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnDepotMapping" runat="server" OnClick="btnDepotMapping_Click" class="h_icon mouse_co" CausesValidation="false" ToolTip='<%# Eval("NAME") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="5" HeaderText="&nbsp;B.SEGMENT&nbsp; MAPPING" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnBSMapping" runat="server" OnClick="btnBSMapping_Click" class="h_icon macos" CausesValidation="false" ToolTip='<%# Eval("NAME") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="5" HeaderText="&nbsp;ITEMTYPE&nbsp; MAPPING" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnItemmapping" runat="server" OnClick="btnItemmapping_Click" class="h_icon megaphone" CausesValidation="false" ToolTip='<%# Eval("NAME") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="5" HeaderText="BRAND &nbsp;MAPPING&nbsp;" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnbrandmapping" runat="server" OnClick="btnbrandmapping_Click" class="h_icon marker" CausesValidation="false" ToolTip='<%# Eval("NAME") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="5" HeaderText="&nbsp;REPORTING&nbsp; MAPPING" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnmenumapping" runat="server" OnClick="btnmenumapping_Click" class="h_icon report_co" CausesValidation="false" ToolTip='<%# Eval("NAME") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div id="DivFooterRow_voucher" style="overflow: hidden">
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
            <script type="text/javascript" language="javascript">
                function CheckAllheadertpu(Checkbox) {
                    var GridVwHeaderCheckbox = document.getElementById("<%=gvPTPUMap.ClientID %>");
                    if (Checkbox.checked) {
                        for (i = 1; i < GridVwHeaderCheckbox.rows.length; i++) {
                            GridVwHeaderCheckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
                            /*GridVwHeaderCheckbox.rows[i].style.color = '#389638';
                            GridVwHeaderCheckbox.rows[i].cells[0].style.border = '1px solid #fff';*/
                        }
                    }
                    else {
                        for (i = 1; i < GridVwHeaderCheckbox.rows.length; i++) {
                            GridVwHeaderCheckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = false;
                            /*GridVwHeaderCheckbox.rows[i].style.color = '#4B555E';
                            GridVwHeaderCheckbox.rows[i].cells[0].style.border = '0px solid gray';*/
                        }
                    }

                }
            </script>
            <script language="javascript" type="text/javascript">
                function MakeStaticHeader_voucher(gridId, height, width, headerHeight, isFooter) {
                    var tbl = document.getElementById(gridId);
                    if (tbl) {
                        var DivHR_inv = document.getElementById('DivHeaderRow_voucher');
                        var DivMC_inv = document.getElementById('DivMainContent_voucher');
                        var DivFR_inv = document.getElementById('DivFooterRow_voucher');

                        //*** Set divheaderRow Properties ****
                        DivHR_inv.style.height = headerHeight + 'px';
                        //DivHR_inv.style.width = (parseInt(width) - 16) + 'px';
                        DivHR_inv.style.width = '98%';
                        DivHR_inv.style.position = 'relative';
                        DivHR_inv.style.top = '0px';
                        DivHR_inv.style.zIndex = '10';
                        DivHR_inv.style.verticalAlign = 'top';

                        //*** Set divMainContent Properties ****
                        DivMC_inv.style.width = width + 'px';
                        DivMC_inv.style.height = height + 'px';
                        DivMC_inv.style.position = 'relative';
                        DivMC_inv.style.top = -headerHeight + 'px';
                        DivMC_inv.style.zIndex = '1';

                        //*** Set divFooterRow Properties ****
                        DivFR_inv.style.width = (parseInt(width) - 16) + 'px';
                        DivFR_inv.style.position = 'relative';
                        DivFR_inv.style.top = -headerHeight + 'px';
                        DivFR_inv.style.verticalAlign = 'top';
                        DivFR_inv.style.paddingtop = '2px';

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
                            DivFR_inv.appendChild(tblfr);
                        }
                        //****Copy Header in divHeaderRow****
                        DivHR_inv.appendChild(tbl.cloneNode(true));
                    }
                }

                function OnScrollDiv_voucher(Scrollablediv) {
                    document.getElementById('DivHeaderRow_voucher').scrollLeft = Scrollablediv.scrollLeft;
                    document.getElementById('DivFooterRow_voucher').scrollLeft = Scrollablediv.scrollLeft;
                }
            </script>
            <script type="text/javascript">
                function isNumberKey(evt) {
                    var charCode = (evt.which) ? evt.which : event.keyCode;
                    if ((charCode < 48 || charCode > 57) && (charCode != 8))
                        return false;

                    return true;
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
            <script type="text/javascript">
                function totaldistributormapped(chb) {
                    var GridVwHeaderCheckbox = document.getElementById("<%=gvdistributor.ClientID %>");
                    var lbltotaldistributorcount = document.getElementById("<%=lbltotaldistributorcount.ClientID %>");
                    var totalcount = 0;
                    var rowData = chb.parentNode.parentNode;
                    var rowIndex = rowData.rowIndex;

                    /*if (GridVwHeaderCheckbox.rows[rowIndex].cells[4].innerHTML.replace('&nbsp;', '').length <= 0) {
                        GridVwHeaderCheckbox.rows[rowIndex].cells[5].getElementsByTagName("INPUT")[0].checked = false;
                    }*/
                    for (i = 0; i < GridVwHeaderCheckbox.rows.length; i++) {
                        if (GridVwHeaderCheckbox.rows[i].cells[5].getElementsByTagName("INPUT")[0].checked == true) {
                            totalcount = totalcount + 1;
                        }
                    }
                    if (totalcount > 0) {
                        lbltotaldistributorcount.style.display = "";
                        lbltotaldistributorcount.innerHTML = 'Total : ' + totalcount + ' party selected.';
                    }
                    else {
                        lbltotaldistributorcount.style.display = "none";
                    }
                }

                function totalbeatmapped(chb) {
                    var GridVwHeaderCheckbox = document.getElementById("<%=gvBeatMap.ClientID %>");
                    var lbltotalbeatcount = document.getElementById("<%=lbltotalbeatcount.ClientID %>");
                    var lbltsiid = document.getElementById("<%=Hdn_Fld.ClientID %>").value;
                    var totalcount = 0;
                    var rowData = chb.parentNode.parentNode;
                    var rowIndex = rowData.rowIndex;
                    var idlblTSIID = "ContentPlaceHolder1_gvBeatMap_lblTSIID_" + rowIndex;
                    var lblchkid = document.getElementById(idlblTSIID).innerHTML;

                    /*if (GridVwHeaderCheckbox.rows[rowIndex].cells[3].innerHTML.replace('&nbsp;', '').length > 0 && lblchkid != lbltsiid) {
                        GridVwHeaderCheckbox.rows[rowIndex].cells[4].getElementsByTagName("INPUT")[0].checked = false;
                    }*/
                    for (i = 0; i < GridVwHeaderCheckbox.rows.length; i++) {
                        if (GridVwHeaderCheckbox.rows[i].cells[4].getElementsByTagName("INPUT")[0].checked == true) {
                            totalcount = totalcount + 1;
                        }
                    }
                    if (totalcount > 0) {
                        lbltotalbeatcount.style.display = "";
                        lbltotalbeatcount.innerHTML = 'Total : ' + totalcount + ' beat selected.';
                    }
                    else {
                        lbltotalbeatcount.style.display = "none";
                    }
                }

                function CheckAllheader(Checkbox) {
                    var GridVwHeaderCheckbox = document.getElementById("<%=gvdistributor.ClientID %>");
                    for (i = 1; i < GridVwHeaderCheckbox.rows.length; i++) {
                        GridVwHeaderCheckbox.rows[i].cells[1].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
                    }
                }
            </script>
            <script language="javascript" type="text/javascript">
                function MakeStaticHeader(gridId, height, width, headerHeight, isFooter) {
                    debugger;
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
            <script type="text/javascript">
                function validateLength(sender, args) {
                    if (args.Value.length < 1)
                        return args.IsValid = false;
                    else
                        return args.IsValid = true;
                }
            </script>
            <style type="text/css">
                .success {
                    font-size: 10px;
                    color: green; /*background-color: #5cb85c;
            padding: 3px 6px 3px 6px;*/
                }

                .failure {
                    font-size: 10px;
                    color: red; /*background-color: #ed4e2a;
            padding: 3px 6px 3px 6px;*/
                }
            </style>
            <script type="text/javascript">
                function checkUserName(txtUserName) {
                    $.ajax({
                        type: "POST",
                        async: true,
                        url: 'frmUserMaster.aspx/CheckUserNameAvailability',
                        data: '{username: "' + $(ContentPlaceHolder1_txtusername).val().trim() + '" }',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            if (response.d != "0") {
                                $("#spnMsg").html(' has already been taken');
                                $("#spnMsg").removeClass("success").addClass("failure");
                                $("#btnsave").prop('disabled', true);

                            }
                            else {
                                $("#spnMsg").html('');
                                $("#spnMsg").removeClass("failure").addClass("success");
                                $("#btnsave").prop('disabled', false);
                            }
                        }
                    });
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>