<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmTransporterMaster.aspx.cs" Inherits="VIEW_frmTransporterMaster" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

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

    <script type="text/javascript" language="javascript">
        function CheckAllheader(Checkbox) {
            var GridVwHeaderCheckbox = document.getElementById("<%=gvStateWiseGst.ClientID %>");
            for (i = 1; i < GridVwHeaderCheckbox.rows.length; i++) {
                GridVwHeaderCheckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
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
    <script type="text/javascript">
        function panno() {
            var checkedValue = document.getElementById("<%= chkgst.ClientID%>");
             if (checkedValue.checked == true) {
                 var panno = document.getElementById("<%=txtpanno.ClientID %>").value;
                 document.getElementById("<%=txtgstpanno.ClientID %>").value = panno;
            }
        }

         /*function chkgst() {
            var checkedValue = document.getElementById("<%= chkgst.ClientID%>");
        if (checkedValue.checked == true) {
            var e = document.getElementById("<%=ddlState.ClientID %>");
                var statevalue = e.options[e.selectedIndex].value;
                document.getElementById("<%=txtstatecode.ClientID %>").value = statevalue;
                var panno = document.getElementById("<%=txtpanno.ClientID %>").value;
                document.getElementById("<%=txtgstpanno.ClientID %>").value = panno;
            }
            else {
                document.getElementById("<%=txtstatecode.ClientID %>").value = "";
                document.getElementById("<%=txtgstpanno.ClientID %>").value = "";
                document.getElementById("<%=txtgstno.ClientID %>").value = "";
        }
        }* /
    </script>


    <asp:UpdateProgress ID="UpdateProgress" runat="server">
        <ProgressTemplate>
            <asp:Image ID="Image1" ImageUrl="../images/loading123.gif" AlternateText="Processing"
                runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
        PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
    <asp:UpdatePanel ID="Update" runat="server" UpdateMode="Always">
        <ContentTemplate>

            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active" id="trBtn" runat="server">
                                <span class="h_icon_he list_images"></span>
                                <h6>Transporter Details</h6>
                                <div class="btn_30_light" style="float: right;" id="btnaddhide" runat="server">
                                    <span class="icon add_co"></span>
                                    <asp:Button ID="btnAddTransporter" runat="server" Text="Add New Record" CssClass="btn_link"
                                        OnClick="btnAddTransporter_Click" CausesValidation="false" />
                                </div>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_title" align="left" width="150">
                                                <asp:Label ID="Label26" Text="COMPANY TYPE" runat="server"></asp:Label>
                                            </td>
                                            <td class="field_input">
                                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td align="left" width="140">
                                                            <asp:DropDownList ID="ddlcompanytype" Width="130" runat="server" class="chosen-select"
                                                                data-placeholder="Select Company" AppendDataBoundItems="True">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td width="60" class="innerfield_title">
                                                            <asp:Label ID="lblCode" Text="Code(Auto Generated)" runat="server"></asp:Label>

                                                        </td>
                                                        <td width="11%">
                                                            <asp:TextBox ID="txtCode" runat="server" CssClass="x-large" MaxLength="20" Enabled="false"></asp:TextBox>
                                                        </td>
                                                        <td width="20">&nbsp;
                                                        </td>
                                                        <td width="140" class="innerfield_title">
                                                            <asp:Label ID="lblName" Text="TRANSPORTER'S NAME" runat="server"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtName" runat="server" CssClass="large" MaxLength="50"> </asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="CV_Name" runat="server" Display="None" ErrorMessage="Transporter Name is required!"
                                                                ControlToValidate="txtName" ValidateEmptyText="false" SetFocusOnError="true" ValidationGroup="TransporterSubmit"> </asp:RequiredFieldValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                                                TargetControlID="CV_Name" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <%--new panel for kkg--%>
                                   
                                        <tr>
                                            <td class="field_title">
                                                <asp:Label ID="lblAdress" Text="Address Info" runat="server"></asp:Label>
                                                <span class="req">*</span>
                                            </td>
                                            <td class="field_input">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td width="170">
                                                            <asp:DropDownList ID="ddlState" runat="server" Width="160" AutoPostBack="true" AppendDataBoundItems="True"
                                                                class="chosen-select" data-placeholder="Select State" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <span class="label_intro">
                                                                <asp:Label ID="Label8" Text="State" runat="server"></asp:Label>
                                                                <asp:RequiredFieldValidator ID="CV_ddlState" runat="server" ErrorMessage="Required!"
                                                                    ForeColor="Red" ControlToValidate="ddlState" ValidateEmptyText="false" SetFocusOnError="true"
                                                                    InitialValue="0" ValidationGroup="TransporterSubmit"> </asp:RequiredFieldValidator></span>
                                                        </td>
                                                        <td width="170">
                                                            <asp:DropDownList ID="ddlDistrict" runat="server" Width="160" AutoPostBack="true"
                                                                AppendDataBoundItems="True" class="chosen-select" data-placeholder="Select District"
                                                                OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" ValidationGroup="TransporterSubmit">
                                                            </asp:DropDownList>
                                                            <span class="label_intro">
                                                                <asp:Label ID="Label9" Text="District" runat="server"></asp:Label>
                                                                <asp:RequiredFieldValidator ID="CV_ddlDistrict" runat="server" ErrorMessage="Required!"
                                                                    ForeColor="Red" ControlToValidate="ddlDistrict" ValidateEmptyText="false" SetFocusOnError="true"
                                                                    InitialValue="0"> </asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server"
                                                                    TargetControlID="CV_ddlDistrict" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="../images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                            </span>
                                                        </td>
                                                        <td width="170">
                                                            <asp:DropDownList ID="ddlCity" runat="server" Width="160" AppendDataBoundItems="true"
                                                                class="chosen-select" data-placeholder="Select City">
                                                            </asp:DropDownList>
                                                            <span class="label_intro">
                                                                <asp:Label ID="Label10" Text="City" runat="server"></asp:Label>
                                                                <asp:RequiredFieldValidator ID="CV_ddlCity" runat="server" ErrorMessage="Required!"
                                                                    ForeColor="Red" ControlToValidate="ddlCity" ValidateEmptyText="false" SetFocusOnError="true"
                                                                    InitialValue="0" ValidationGroup="TransporterSubmit"> </asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server"
                                                                    TargetControlID="CV_ddlCity" PopupPosition="BottomLeft" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="../images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                            </span>
                                                        </td>
                                                        <td valign="top">
                                                            <asp:Button ID="btnAddCity" runat="server" CssClass="h_icon add_co" ToolTip="Add City"
                                                                OnClick="btnAddCity_Click" CausesValidation="false" Enabled="false" />
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
                                                            <asp:TextBox ID="txtAddress" runat="server" CssClass="large" TextMode="MultiLine"
                                                                MaxLength="255"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="CV_txtAddress" runat="server" Display="None" ErrorMessage="Address is required!"
                                                                ControlToValidate="txtAddress" ValidateEmptyText="false" SetFocusOnError="true" ValidationGroup="TransporterSubmit"> </asp:RequiredFieldValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server"
                                                                TargetControlID="CV_txtAddress" PopupPosition="BottomLeft" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                            <span class="label_intro">Contact Address</span>
                                                        </td>
                                                        <td valign="top" style="padding: 0 0 0 15px;">
                                                            <asp:TextBox ID="txtPIN" runat="server" MaxLength="6" CssClass="mid" onkeypress="return isNumberKey(event);"> </asp:TextBox>

                                                            <span class="label_intro">Pin code</span>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title">
                                                <asp:Label ID="Label3" Text="Alternate Address Info" runat="server"></asp:Label>
                                            </td>
                                            <td class="field_input">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td colspan="2" height="2"></td>
                                                    </tr>
                                                    <tr>
                                                        <td width="40%">
                                                            <asp:TextBox ID="txtaddress2" runat="server" CssClass="large" TextMode="MultiLine"
                                                                MaxLength="255"></asp:TextBox>
                                                            <span class="label_intro">Contact Address</span>
                                                        </td>
                                                        <td valign="top" style="padding: 0 0 0 15px;">
                                                            <asp:TextBox ID="txtpin2" runat="server" MaxLength="6" CssClass="mid" onkeypress="return isNumberKey(event);"> </asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtPIN"
                                                                ErrorMessage="Minimum 6 digits PIN/ZIP required." ValidationExpression="[0-9]{6}"
                                                                Display="None" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender28" runat="server"
                                                                TargetControlID="RegularExpressionValidator6" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                            <span class="label_intro">Pin code</span>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title">Contact Person
                                            </td>
                                            <td class="field_input">
                                                <asp:TextBox ID="txtcontactperson" runat="server" CssClass="mid" MaxLength="50"></asp:TextBox>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title">Contact Info <span class="req">*</span>
                                            </td>
                                            <td class="field_input">
                                                <table border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td width="22%">
                                                            <asp:TextBox ID="txtMobileNo" runat="server" MaxLength="10" onkeypress="return isNumberKey(event);"></asp:TextBox>

                                                            <span class=" label_intro">Mobile No</span>
                                                        </td>
                                                        <td width="15">&nbsp;
                                                        </td>
                                                        <td width="22%">
                                                            <asp:TextBox ID="txtPhoneNo" runat="server" MaxLength="11" onkeypress="return isNumberKey(event);"></asp:TextBox>

                                                            <span class=" label_intro">LandLine No</span>
                                                        </td>
                                                        <td width="15">&nbsp;
                                                        </td>
                                                        <td width="22%">
                                                            <asp:TextBox ID="txtMobileNo1" runat="server" MaxLength="10" onkeypress="return isNumberKey(event);"></asp:TextBox>

                                                            <span class=" label_intro">Mobile No 2</span>
                                                        </td>
                                                        <td width="15">&nbsp;
                                                        </td>
                                                        <td width="22%">
                                                            <asp:TextBox ID="txtPhoneNo1" runat="server" MaxLength="11" onkeypress="return isNumberKey(event);"></asp:TextBox>

                                                            <span class=" label_intro">LandLine No 2</span>
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title">
                                                <asp:Label ID="lblemailid" Text="EMAIL ID" runat="server"></asp:Label>
                                            </td>
                                            <td class="field_input">
                                                <asp:TextBox ID="txtEmailid" runat="server" CssClass="mid"> </asp:TextBox>

                                                <asp:RegularExpressionValidator ID="CV_email_valid" runat="server" Display="None"
                                                    ErrorMessage="Enter Valid Email ID!" ControlToValidate="txtEmailid" SetFocusOnError="true"
                                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="TransporterSubmit"> 
                                                </asp:RegularExpressionValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                                    TargetControlID="CV_email_valid" PopupPosition="Right" HighlightCssClass="errormassage"
                                                    WarningIconImageUrl="../images/050.png">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title">Company Info
                                            </td>
                                            <td class="field_input">
                                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td width="22%">
                                                            <asp:TextBox ID="txtcstno" runat="server" CssClass="x-large" MaxLength="20"> </asp:TextBox>

                                                            <span class="label_intro">
                                                                <asp:Label ID="lblcstno" Text="CST NO" runat="server"></asp:Label></span>
                                                        </td>
                                                        <td width="15">&nbsp;
                                                        </td>
                                                        <td width="22%">
                                                            <asp:TextBox ID="txtvatno" runat="server" CssClass="x-large" MaxLength="20"> </asp:TextBox>

                                                            <span class="label_intro">
                                                                <asp:Label ID="lblvatno" Text="VAT NO" runat="server"></asp:Label></span>
                                                        </td>
                                                        <td width="15">&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txttinno" runat="server" CssClass="mid" MaxLength="20"> </asp:TextBox>

                                                            <span class="label_intro">
                                                                <asp:Label ID="lbltinno" Text="TIN NO" runat="server"></asp:Label></span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtpanno" runat="server" CssClass="x-large"  onkeypress="return isLetterNumberKey(event);"
                                                                MaxLength="10" onchange="CHKPAN();" Style='text-transform: uppercase' onkeyup="panno()"> </asp:TextBox>

                                                            <%-- <asp:RegularExpressionValidator ID="tfv_valid_panno" runat="server" ControlToValidate="txtpanno"  
                                                                    Display="none" ForeColor="Red" ErrorMessage="InValid PAN No" 
                                                                    ValidationExpression="[A-Z]{5}\d{4}[A-Z]{1}" ValidationGroup="TransporterSubmit">
                                                            </asp:RegularExpressionValidator>   not working and closed on 14.12.2017--%>
                                                            <%--<ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server"
                                                                        TargetControlID="tfv_valid_panno" PopupPosition="BottomLeft" HighlightCssClass="errormassage"
                                                                        WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>--%>

                                                            <%-- <asp:RequiredFieldValidator ID="rfv_panno" runat="server" Display="None" ErrorMessage="Required!"
                                                                ControlToValidate="txtpanno" ValidateEmptyText="false" SetFocusOnError="true" ValidationGroup="TransporterSubmit"> </asp:RequiredFieldValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                                                                TargetControlID="rfv_panno" PopupPosition="BottomLeft" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png"/>
                                                            <span class="label_intro">--%>
                                                            <asp:Label ID="lblpanno" Text="PAN NO &nbsp;(ex:AAAAA9999A)" runat="server"></asp:Label></span>
                                                        </td>
                                                        <td width="15">&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSTNo" runat="server" CssClass="x-large" MaxLength="20"> </asp:TextBox>

                                                            <span class="label_intro">
                                                                <asp:Label ID="Label1" Text="ST NO" runat="server"></asp:Label></span>
                                                        </td>
                                                        <td width="15">&nbsp;
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title">Bank Info
                                            </td>
                                            <td class="field_input">
                                                <table border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td width="22%">
                                                            <asp:TextBox ID="txtbankacno" runat="server" CssClass="x-large" onkeypress="return isNumberKey(event);"
                                                                MaxLength="25"></asp:TextBox>

                                                            <span class="label_intro">
                                                                <asp:Label ID="lblbankacno" Text="Bank Account No" runat="server"></asp:Label></span>
                                                        </td>
                                                        <td width="15">&nbsp;
                                                        </td>
                                                        <td width="22%">
                                                            <asp:TextBox ID="txtIFSC" runat="server" CssClass="x-large" MaxLength="20"></asp:TextBox>

                                                            <span class="label_intro">
                                                                <asp:Label ID="lblIfsc" Text="IFSC Code" runat="server"></asp:Label></span>
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" height="10"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:DropDownList ID="ddlbankname" Width="200" runat="server" data-placeholder="Select Bank Name"
                                                                AppendDataBoundItems="True" class="chosen-select" />
                                                            <span class="label_intro">
                                                                <asp:Label ID="Label11" Text="Bank Name" runat="server"></asp:Label></span>

                                                        </td>
                                                        <td width="15">&nbsp;
                                                        </td>
                                                        <td width="210">
                                                            <asp:DropDownList ID="ddlbranchname" Width="200" runat="server" data-placeholder="Select Branch Name"
                                                                class="chosen-select" AppendDataBoundItems="True" />
                                                            <span class="label_intro">
                                                                <asp:Label ID="Label12" Text="Branch Name" runat="server"></asp:Label></span>

                                                        </td>
                                                        <td align="left" valign="top">
                                                            <asp:Button ID="ButtonAdd" runat="server" CssClass="h_icon add_co" ToolTip="Add Branch"
                                                                OnClick="btnAddBranch_Click" CausesValidation="false" Enabled="true" />
                                                            <asp:Button ID="ButtonRefresh1" runat="server" CssClass="h_icon arrow_refresh_co"
                                                                ToolTip="Refresh" OnClick="btnRefresh1_Click" CausesValidation="false" Enabled="true" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title">&nbsp;
                                            </td>
                                            <td class="field_input">
                                                <asp:RadioButtonList ID="rdbledger" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdbledger_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                    <asp:ListItem Value='0' Text="Create Ledger" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value='1' Text="Map Existing Ledger"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr id="trgroup" runat="server">
                                            <td class="field_title">ACCOUNTS GROUP <span class="req">*</span>
                                            </td>
                                            <td class="field_input">
                                                <table border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <asp:DropDownList ID="ddlAccGroup" Width="200" runat="server" data-placeholder="Select Group Name"
                                                                AppendDataBoundItems="True" class="chosen-select" />

                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr id="trledger" runat="server">
                                            <td class="field_title">Ledger <span class="req">*</span>
                                            </td>
                                            <td class="field_input">
                                                <table border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <asp:DropDownList ID="ddlledger" Width="200" runat="server" data-placeholder="Select Ledger"
                                                                AppendDataBoundItems="True" class="chosen-select" />

                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title">IS GST Applicable
                                            </td>
                                            <td class="field_input">
                                                <table border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td width="50">
                                                            <asp:CheckBox ID="chkgst" runat="server" Text=" " AutoPostBack="true" OnCheckedChanged="chkgst_CheckedChanged" /></td>
                                                        <td width="70">
                                                            <asp:Label ID="Label6" CssClass="innerfield_title" Text="GSTIN" runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="txtstatecode" runat="server" MaxLength="15" Enabled="false" Width="15px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtgstpanno" runat="server" MaxLength="15" Enabled="false" Width="80px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                         
                                                            <asp:TextBox ID="txtgstno"  Style='text-transform: uppercase' onchange="CHKGSTM();"
                                                                 onkeyup="return isLetterNumberKey3D(event);"
                                                                 onkeypress="return isLetterNumberKey(event);" 
                                                                runat="server" MaxLength="03" Width="30"></asp:TextBox>

                                                        </td>

                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td class="field_title">TDS LIMIT 
                                            </td>
                                            <td class="field_input">
                                                <asp:TextBox ID="txttdslimit" runat="server" Width="260"></asp:TextBox>
                                            </td>



                                        </tr>
                                        <tr>
                                            <td class="field_title">Others Applicable
                                            </td>
                                            <td class="field_input">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td width="95">
                                                            <asp:Label ID="Label2" CssClass="innerfield_title" Text="TDS Applicable" runat="server"></asp:Label>
                                                        </td>
                                                        <td width="100">
                                                            <asp:CheckBox ID="chkTDS" runat="server" Text=" " />
                                                        </td>
                                                        <td width="110">
                                                            <asp:Label ID="Label4" CssClass="innerfield_title" Text="IS S.TAX APPLICABLE" runat="server"></asp:Label>
                                                        </td>
                                                        <td width="100">
                                                            <asp:CheckBox ID="chktax" runat="server" Text=" " />
                                                        </td>
                                                        <td width="60">
                                                            <asp:Label ID="lblactive" CssClass="innerfield_title" Text="ACTIVE" runat="server"></asp:Label>
                                                        </td>
                                                        <td width="50">
                                                            <asp:CheckBox ID="chkActive" runat="server" Text=" " />
                                                        </td>
                                                        <td width="100">
                                                            <asp:Label ID="Label7" CssClass="innerfield_title" Text="Transfer To HO" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox ID="chkTransferToHO" runat="server" Text=" " />
                                                        </td>
                                                        <td width="110">
                                                            <asp:Label ID="Label14" CssClass="innerfield_title" Text="Reverse Charge" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox ID="ChkReverseCharge" runat="server" Text=" " />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title">
                                                <asp:Label ID="lblrdb" runat="server" Text="ACCOUNTING POSTING HO"></asp:Label>
                                                <span class="req">*</span>
                                            </td>
                                            <td class="field_input">
                                                <asp:RadioButtonList ID="rdbaccount" runat="server" ForeColor="blue" RepeatDirection="Horizontal"
                                                    Width="150px">
                                                    <asp:ListItem Text="YES" Value="Y" />
                                                    <asp:ListItem Text="NO" Value="N" Selected="True" />
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title">
                                                <asp:Label ID="Label5" runat="server" Text="MORE THAN 10 VECHILES"></asp:Label>
                                                <span class="req">*</span>
                                            </td>
                                            <td class="field_input">
                                                <asp:RadioButtonList ID="rdbvechile" runat="server" ForeColor="blue" RepeatDirection="Horizontal"
                                                    Width="150px">
                                                    <asp:ListItem Text="YES" Value="Y" />
                                                    <asp:ListItem Text="NO" Value="N" Selected="True" />
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            
                                            <td class="field_input" colspan="2">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td width="95">
                                                            <asp:Label ID="Label15" CssClass="innerfield_title" Text="&nbsp;&nbsp;&nbsp;&nbsp;Credit Limit" runat="server"></asp:Label>
                                                        </td>
                                                        <td width="100">
                                                            <asp:TextBox ID="txtcreditlimit" runat="server" MaxLength="10" Style="width: 100px;"></asp:TextBox>
                                                        </td>
                                                        <td width="110">
                                                            <asp:Label ID="Label16" CssClass="innerfield_title" Text="&nbsp;&nbsp;&nbsp;&nbsp;Credit Day(S)" runat="server"></asp:Label>
                                                        </td>
                                                        <td width="100">
                                                            <asp:TextBox ID="txtcreditday" runat="server" MaxLength="5" Style="width: 100px;"></asp:TextBox>
                                                        </td>
                                                        <td width="60">&nbsp;
                                                        </td>
                                                        <td width="50">&nbsp;
                                                        </td>
                                                        <td width="100">&nbsp;
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                        <td width="110">&nbsp;
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                          <tr id="Tr2" runat="server">
                                            <td align="left" class="field_title">MSME<span class="req">*</span>
                                            </td>
                                            <td align="left" class="field_input">
                                               <asp:RadioButtonList ID="RbApplicable" AutoPostBack="true" RepeatDirection="Horizontal" 
                                                    runat="server"  onClick="toggleHideTr();" OnSelectedIndexChanged="RbApplicable_SelectedIndexChanged">
                                                    <asp:ListItem Value="Y">YES</asp:ListItem>
                                                    <asp:ListItem Value="N">NO</asp:ListItem>
                                              </asp:RadioButtonList>

                                            </td>
                                        </tr>
                                         <tr>
                                            <td align="left" class="field_title" id="tdlblApplicable" runat="server">
                                                <asp:Label ID="lblApplicable" runat="server" /><span class="req">*</span></td>
                                            <td class="field_input" id="tdlblApplicable2" runat="server">                                                
                                                <asp:TextBox ID="ddlMsmeNo" runat="server" TabIndex="4"
                                                    Width="180" AppendDataBoundItems="True"></asp:TextBox>
                                                <asp:CustomValidator ID="cvddlMsmeNo" runat="server" ValidateEmptyText="true"
                                                    ControlToValidate="ddlMsmeNo" ValidationGroup="TPUSubmit" ErrorMessage="Required!"
                                                    Display="Dynamic" ClientValidationFunction="ValidateListBox" ForeColor="Red"></asp:CustomValidator>
                                               <asp:Label ID="lblApplicable2" runat="server" class="field_title"/><span class="req">*</span>
                                                  <asp:TextBox ID="ddlMsmedate" runat="server" Enabled="false" Width="110" placeholder="dd/MM/yyyy"
                                                         MaxLength="10"></asp:TextBox>
                                                     <asp:ImageButton ID="imgbtnLRGRCalendar" runat="server" ImageUrl="~/images/calendar.png"
                                                         Width="24px" Height="24px" ImageAlign="AbsMiddle" />
                                                     <ajaxToolkit:CalendarExtender ID="CalendarExtenderLRGRDate" TargetControlID="ddlMsmedate"
                                                         PopupButtonID="imgbtnLRGRCalendar" runat="server" Format="dd/MM/yyyy" Enabled="true"
                                                         BehaviorID="CalendarExtenderLRGRDate" CssClass="cal_Theme1" />
                                                           
                                            </td>

                                        </tr>
                                       
                                        <tr>
                                            <td>&nbsp;
                                            </td>
                                            <td style="padding: 8px 0px;">
                                                <div class="btn_24_blue">
                                                    <span class="icon disk_co"></span>
                                                    <asp:Button ID="btntransporterSubmit" runat="server" Text="Save" CssClass="btn_link" ValidationGroup="TransporterSubmit"
                                                        OnClick="btntransporterSubmit_Click" OnClientClick="Confirm()" />
                                                </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_blue">
                                                    <span class="icon cross_octagon_co"></span>
                                                    <asp:Button ID="btnTransporterCancel" runat="server" Text="Cancel" CssClass="btn_link"
                                                        OnClick="btnTransporterCancel_Click" CausesValidation="false" />
                                                </div>
                                                <asp:HiddenField ID="Hdn_Fld" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>

                                <asp:Panel ID="pnlmapping" runat="server">
                                    <table border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                        <tr>
                                            <td width="130" class="field_title">
                                                <asp:Label ID="lblPO" Text="TRANSPORTER NAME" runat="server"></asp:Label><span class="req">*</span>
                                            </td>
                                            <td class="field_input">
                                                <asp:TextBox ID="txtTname" runat="server" CssClass="required large" Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="trBSTPU">
                                            <td class="field_input" style="padding-left: 10px;" colspan="2">
                                                <fieldset>
                                                    <legend>Factory/TPU/Vendor MAPPING</legend>
                                                    <table border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                                        <tr>
                                                            <td valign="top">
                                                                <cc1:Grid ID="gvTPUMap" runat="server" CallbackMode="false" Serialize="true" FolderStyle="../GridStyles/premiere_blue"
                                                                    AutoGenerateColumns="false" AllowPageSizeSelection="true" AllowRecordSelection="false"
                                                                    AllowPaging="false" AllowAddingRecords="false" AllowFiltering="false" OnRowDataBound="gvTPUMap_RowDataBound"
                                                                    AllowSorting="false" PageSize="1500" CurrentPageIndex="1500" PageSizeOptions="10,50,100,250,500,1000,1500">
                                                                    <FilteringSettings InitialState="Visible" />
                                                                    <Columns>
                                                                        <cc1:Column ID="Column3" DataField="VENDORNAME" HeaderText="TPU/Factory/Vendor MAPPING"
                                                                            runat="server" Width="250">
                                                                            <FilterOptions>
                                                                                <cc1:FilterOption Type="NoFilter" />
                                                                                <cc1:FilterOption Type="Contains" />
                                                                                <cc1:FilterOption Type="StartsWith" />
                                                                            </FilterOptions>
                                                                        </cc1:Column>
                                                                        <cc1:Column ID="Column4" DataField="VENDORID" ReadOnly="true" HeaderText="ID" Width="100"
                                                                            runat="server">
                                                                            <TemplateSettings TemplateId="CheckTemplateTPU" HeaderTemplateId="HeaderTemplateTPU" />
                                                                        </cc1:Column>
                                                                    </Columns>
                                                                    <FilteringSettings MatchingType="AnyFilter" />
                                                                    <Templates>
                                                                        <cc1:GridTemplate runat="server" ID="CheckTemplateTPU">
                                                                            <Template>
                                                                                <asp:HiddenField runat="server" ID="hdnTPUName" Value='<%# Container.DataItem["VENDORNAME"] %>' />
                                                                                <asp:CheckBox ID="ChkIDTPU" runat="server" Text=" " ToolTip="<%# Container.Value %>" />
                                                                            </Template>
                                                                        </cc1:GridTemplate>
                                                                    </Templates>
                                                                    <ScrollingSettings ScrollWidth="100%" ScrollHeight="250" />
                                                                </cc1:Grid>
                                                            </td>
                                                            <td>&nbsp;
                                                            </td>
                                                            <td valign="top">
                                                                <cc1:Grid ID="gvDepot" runat="server" CallbackMode="false" Serialize="true" FolderStyle="../GridStyles/premiere_blue"
                                                                    AutoGenerateColumns="false" AllowPageSizeSelection="true" AllowRecordSelection="false"
                                                                    AllowPaging="false" AllowAddingRecords="false" AllowFiltering="false" OnRowDataBound="gvDepot_RowDataBound"
                                                                    AllowSorting="false" PageSize="500">
                                                                    <FilteringSettings InitialState="Visible" />
                                                                    <Columns>
                                                                        <cc1:Column ID="Column7" DataField="BRNAME" HeaderText="Depot. MAPPING" runat="server"
                                                                            Width="250">
                                                                            <FilterOptions>
                                                                                <cc1:FilterOption Type="NoFilter" />
                                                                                <cc1:FilterOption Type="Contains" />
                                                                                <cc1:FilterOption Type="StartsWith" />
                                                                            </FilterOptions>
                                                                        </cc1:Column>
                                                                        <cc1:Column ID="Column8" DataField="BRID" ReadOnly="true" HeaderText="ID" Width="100"
                                                                            runat="server">
                                                                            <TemplateSettings TemplateId="CheckTemplateFactory" HeaderTemplateId="HeaderTemplateFactory" />
                                                                        </cc1:Column>
                                                                    </Columns>
                                                                    <FilteringSettings MatchingType="AnyFilter" />
                                                                    <Templates>
                                                                        <cc1:GridTemplate runat="server" ID="CheckTemplateFactory">
                                                                            <Template>
                                                                                <asp:HiddenField runat="server" ID="hdnFactoryName" Value='<%# Container.DataItem["BRNAME"] %>' />
                                                                                <asp:CheckBox ID="ChkIDFactory" runat="server" Text=" " ToolTip="<%# Container.Value %>" />
                                                                                <%--<asp:CheckBox ID="ChkIDTPU" runat="server" ToolTip="<%# Container.Value %>" />--%>
                                                                            </Template>
                                                                        </cc1:GridTemplate>
                                                                    </Templates>
                                                                    <ScrollingSettings ScrollWidth="100%" ScrollHeight="250" />
                                                                </cc1:Grid>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="trbtnBSTPU">
                                            <td colspan="2" class="field_input" style="padding-left: 10px;">
                                                <div class="btn_24_blue">
                                                    <span class="icon disk_co"></span>
                                                    <asp:Button ID="btnBSTPUSave" runat="server" Text="Save" CssClass="btn_link" CausesValidation="false"
                                                        OnClick="btnBSTPUSave_Click" />
                                                </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_blue">
                                                    <span class="icon cross_octagon_co"></span>
                                                    <asp:Button ID="btnBSTPUCancel" runat="server" Text="Cancel" CssClass="btn_link"
                                                        CausesValidation="false" OnClick="btnBSTPUCancel_Click" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>

                                <asp:Panel ID="pnlStateWiseGstMapping" runat="server">
                                    <table border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                        <tr>
                                            <td width="80" class="field_title">
                                                <asp:Label ID="Label13" Text="TRANSPORTER NAME" runat="server"></asp:Label>
                                            </td>
                                            <td class="field_input" width="130">
                                                <asp:TextBox ID="txttrasnsporternamegst" runat="server" CssClass="x-large" Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="tr1">
                                            <td class="field_input" style="padding-left: 10px;" colspan="2">
                                                <fieldset>
                                                    <legend>STate Wise GST MAPPING</legend>
                                                    <table border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                                        <tr>
                                                            <td valign="top">
                                                                <div style="margin: 0 auto; width: 700PX;">
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

                                                                                <asp:TemplateField HeaderText="State Name" ItemStyle-Width="170px" HeaderStyle-Width="170px">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblState_Name" runat="server" Text='<%# Bind("State_Name") %>'
                                                                                            value='<%# Eval("State_Name") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>


                                                                                <asp:TemplateField HeaderText="GST NO" ItemStyle-Width="150px" HeaderStyle-Width="150px">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtgstnostatewise" runat="server" Text='<%# Bind("GSTNO") %>' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="GST ADDRESS" ItemStyle-Width="100px" HeaderStyle-Width="100px">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtgstaddress" runat="server" Text='<%# Bind("GSTADDRESS") %>' />
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
                                        <tr>
                                            <td colspan="2" class="field_input" style="padding-left: 10px;">
                                                <div class="btn_24_blue">
                                                    <span class="icon disk_co"></span>
                                                    <asp:Button ID="btnStateWiseSave" runat="server" Text="Save" CssClass="btn_link" CausesValidation="false"
                                                        OnClick="btnStateWiseSave_Click" />
                                                </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_blue">
                                                    <span class="icon cross_octagon_co"></span>
                                                    <asp:Button ID="btnStateWiseCancel" runat="server" Text="Cancel" CssClass="btn_link"
                                                        CausesValidation="false" OnClick="btnStateWiseCancel_Click" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlDisplay" runat="server">
                                    <tr>
                                        <td>
                                            <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                                <td width="60" class="field_title">
                                                    <asp:Label ID="lblstate" runat="server" Text="State"></asp:Label>
                                                </td>
                                                <td width="150" class="field_title">
                                                    <asp:DropDownList ID="ddlsearchstate" runat="server" AutoPostBack="true" Width="150"
                                                        class="chosen-select" data-placeholder="Select State" OnSelectedIndexChanged="ddlsearchstate_SelectedIndexChanged"
                                                        AppendDataBoundItems="true">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>&nbsp;
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
                                        <cc1:Grid ID="gvTransporter" runat="server" CallbackMode="false" Serialize="true"
                                            FolderStyle="../GridStyles/premiere_blue" AutoGenerateColumns="false" OnRowDataBound="gvTransporter_RowDataBound"
                                            AllowPageSizeSelection="true" PageSize="500" OnDeleteCommand="DeleteRecord" AllowAddingRecords="false"
                                            AllowFiltering="true" OnExported="gvTransporter_Exported" OnExporting="gvTransporter_Exporting">
                                            <ScrollingSettings ScrollHeight="290" ScrollWidth="100" />
                                            <ExportingSettings ExportColumnsFooter="true" AppendTimeStamp="false" ExportGroupFooter="true"
                                                ExportGroupHeader="true" FileName="TransporterList" ExportAllPages="true" ExportDetails="true" />
                                            <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size: 10px;background-color: #DCE3E8;color: #0000FF;"
                                                CSSExportCellStyle="font-weight: normal;font-size: 10px;color: #0000;background-color: AFEEEE" />
                                            <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" OnClientDelete="OnDelete" />
                                            <FilteringSettings InitialState="Visible" />
                                            <Columns>
                                                <cc1:Column DataField="SLNO" ReadOnly="true" HeaderText="SLNO" runat="server" Width="80px" />
                                                <cc1:Column DataField="ID" ReadOnly="true" HeaderText="ID" runat="server" Visible="false" />
                                                <cc1:Column DataField="CODE" HeaderText=" CODE" runat="server" Width="90px">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column DataField="NAME" HeaderText="TRANSPORTER'S NAME" runat="server" Width="280px">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column16" DataField="COMPANY" HeaderText="COMPANY TYPE" runat="server" Width="280px">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column13" DataField="GSTNO" HeaderText="GST NO" runat="server" Width="120px">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column DataField="ADDRESS" HeaderText="ADDRESS" runat="server" Width="230px" Visible="false">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column9" DataField="PINZIP" HeaderText="PIN/ZIP" runat="server" Visible="false">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column1" DataField="State_Name" HeaderText="STATE" runat="server"
                                                    Width="130px">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column2" DataField="DISTRICTNAME" HeaderText="DISTRICT" runat="server"
                                                    Visible="false">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column5" DataField="CITYNAME" HeaderText="CITY" runat="server" Width="140px">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column6" DataField="CONTACTPERSON" HeaderText="CONTACTPERSON" runat="server"
                                                    Visible="false">
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
                                                <cc1:Column DataField="EMAILID" HeaderText="EMAIL ID" runat="server" Width="160"
                                                    Visible="false">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column10" DataField="PANNO" HeaderText="PAN NO." runat="server" Width="100">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column11" DataField="TINNO" HeaderText="TIN NO." runat="server" Width="160"
                                                    Visible="false">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column DataField="ACCGROUPNAME" HeaderText="ACCOUNTS GROUP"
                                                    runat="server" Wrap="true" Width="90">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column14" DataField="REVERSECHARGE" HeaderText="REVERSE CHARGE"
                                                    runat="server" Wrap="true" Width="90">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column12" DataField="ISTDSDECLARE" HeaderText="TDS DECLARE(APPLICABLE)"
                                                    runat="server" Wrap="true" Width="90">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column DataField="SERVICETAX" HeaderText="SERVICE TAX(APPLICABLE)" runat="server"
                                                    Wrap="true" Width="90">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column15" DataField="ACTIVE_STATUS" HeaderText="STATUS" runat="server"
                                                    Wrap="true" Width="90">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column AllowEdit="true" AllowDelete="true" HeaderText="EDIT" runat="server"
                                                    Width="50">
                                                    <TemplateSettings TemplateId="editBtnTemplate" />
                                                </cc1:Column>
                                                <cc1:Column HeaderText="DELETE" AllowEdit="false" AllowDelete="true" Width="60">
                                                    <TemplateSettings TemplateId="deleteBtnTemplate" />
                                                </cc1:Column>
                                                <cc1:Column HeaderText="TPU/FACTORY/DEPOT MAPPING " AllowEdit="false" AllowDelete="true" Width="100"
                                                    Wrap="true">
                                                    <TemplateSettings TemplateId="MappingTemplate" />
                                                </cc1:Column>
                                                <cc1:Column HeaderText="STATE WISE GST MAPPING " AllowEdit="false" AllowDelete="true" Width="100"
                                                    Wrap="true">
                                                    <TemplateSettings TemplateId="SateWiseGstMappingTemplate" />
                                                </cc1:Column>
                                                <%-- <cc1:Column HeaderText="Depot Mapping " AllowEdit="false" AllowDelete="true" Width="100" Wrap="true">
                                                <TemplateSettings TemplateId="MappingTemplatePackSize" />
                                            </cc1:Column>--%>
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
                                                        <a href="javascript: //" class="action-icons c-delete" title="Delete" onclick="gvTransporter.delete_record(this)"></a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="MappingTemplate">
                                                    <Template>
                                                        <a href="javascript: //" class="h_icon map" title="Depot & TPU Mapping" onclick="openMapping('<%# Container.DataItem["NAME"] %>','<%# Container.DataItem["ID"] %>')" />
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="SateWiseGstMappingTemplate">
                                                    <Template>
                                                        <a href="javascript: //" class="h_icon map" title="STATE WISE GST MAPPING" onclick="SateWiseGstMapping('<%# Container.DataItem["NAME"] %>','<%# Container.DataItem["ID"] %>')" />
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <ScrollingSettings ScrollWidth="100%" ScrollHeight="250" NumberOfFixedColumns="4" />
                                        </cc1:Grid>
                                        <asp:HiddenField ID="hdnTid" runat="server" />
                                        <asp:Button ID="btngridedit" runat="server" Text="Gridedit" Style="display: none"
                                            OnClick="btngridedit_Click" CausesValidation="false" />

                                        <asp:Button ID="btnSateWiseGst" runat="server" Text="Gridedit" Style="display: none"
                                            OnClick="btnSateWiseGst_Click" CausesValidation="false" />
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
                function exportToExcel() {
                    gvTransporter.exportToExcel();
                }
            </script>
            <script type="text/javascript">
                function OnBeforeDelete(record) {
                    record.Error = '';
                    document.getElementById("<%=Hdn_Fld.ClientID %>").value = record.TRANSPOTERID;
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
                    document.getElementById("<%=hdnTid.ClientID %>").value = '';
                    var iRecordIndex = oLink.id.toString().replace("btnGridEdit_", "");
                    document.getElementById("<%=Hdn_Fld.ClientID %>").value = gvTransporter.Rows[iRecordIndex].Cells[1].Value;
                    document.getElementById("<%=btngridedit.ClientID %>").click();
                }

                function openMapping(tname, tid) {
                    document.getElementById("<%=hdnTid.ClientID %>").value = tid;
                    document.getElementById("<%=txtTname.ClientID %>").value = tname;
                    document.getElementById("<%=btngridedit.ClientID %>").click();
                }

                function SateWiseGstMapping(tname, tid) {
                    document.getElementById("<%=hdnTid.ClientID %>").value = tid;
                    document.getElementById("<%=txttrasnsporternamegst.ClientID %>").value = tname;
                    document.getElementById("<%=btnSateWiseGst.ClientID %>").click();
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
                    gvTransporter.addFilterCriteria('CODE', OboutGridFilterCriteria.Contains, searchValue);
                    gvTransporter.addFilterCriteria('NAME', OboutGridFilterCriteria.Contains, searchValue);
                    gvTransporter.addFilterCriteria('ADDRESS', OboutGridFilterCriteria.Contains, searchValue);
                    gvTransporter.addFilterCriteria('PHONENO', OboutGridFilterCriteria.Contains, searchValue);
                    gvTransporter.addFilterCriteria('MOBILENO', OboutGridFilterCriteria.Contains, searchValue);
                    gvTransporter.addFilterCriteria('EMAILID', OboutGridFilterCriteria.Contains, searchValue);
                    gvTransporter.addFilterCriteria('PINZIP', OboutGridFilterCriteria.Contains, searchValue);
                    gvTransporter.addFilterCriteria('ISAPPROVED', OboutGridFilterCriteria.Contains, searchValue);

                    gvTransporter.executeFilter();
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
                         return ((keyCode >= 48 && keyCode <= 57)||(keyCode >= 65 && keyCode <= 90) || (keyCode >= 97 && keyCode <= 122))

                        return true;                     
                    };
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>