<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmMiscellaneousProduct_FAC.aspx.cs" Inherits="VIEW_frmMiscellaneousProduct_FAC" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../css/bootstrap-multiselect.css" rel="stylesheet" />
    <script type="text/javascript" src="../js/bootstrap.js"></script>
    <script type="text/javascript" src="../js/bootstrap-multiselect.js"></script>
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
        window.onload = function () {
            oboutGrid.prototype.selectRecordByClick = function () {
                return;
            }
            oboutGrid.prototype.showSelectionArea = function () {
                return;
            }
        }
    </script>
    <script type="text/javascript">
        // check/uncheck all rows.
        function checkAll() {
            var newState = document.getElementById("chk_all").checked;

            var inputArr = document.getElementsByTagName("INPUT");
            for (i = 0; i < inputArr.length; i++) {
                var e = inputArr[i];
                if (e && e.type == "checkbox" && e.id != null && e.id.indexOf("chk_grid_") == 0) {
                    e.checked = newState;

                    var oElement = e.parentNode.parentNode.parentNode;
                    var oContainer = oElement.parentNode;
                    if (oElement && oContainer) {
                        var iRecordIndex = -1;
                        for (var j = 0; j < oContainer.childNodes.length; j++) {
                            if (oContainer.childNodes[j] == oElement) {
                                iRecordIndex = j;
                                break;
                            }
                        }

                        if (iRecordIndex != -1) {
                            if (newState == true) {
                                // select the record
                                grid1.selectRecord(iRecordIndex);
                            } else {
                                // deselect the record
                                grid1.deselectRecord(iRecordIndex);
                            }
                        }
                    }
                }
            }
        }
    </script>
    <script type="text/javascript">
        function ValidateListBox_VendorMap(sender, args) {
            var options = document.getElementById("<%=ddlVendorMap.ClientID%>").options;
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
        function ValidateListBox_Customer(sender, args) {
            var options = document.getElementById("<%=ddlcustomermap.ClientID%>").options;
            for (var i = 0; i < options.length; i++) {
                if (options[i].selected == true) {
                    args.IsValid = true;
                    return;
                }
            }
            args.IsValid = false;
        }
    </script>

    <%--<script type="text/javascript">        
        $(function () {
            $('#ContentPlaceHolder1_ddlfactorymap').multiselect({
                includeSelectAllOption: true
            });
            $("#ContentPlaceHolder1_ddlfactorymap").multiselect('selectAll', false);
            $("#ContentPlaceHolder1_ddlfactorymap").multiselect('updateButtonText');
        });
    </script>--%>

    <script type="text/javascript">        
        $(function () {
            $('#ContentPlaceHolder1_ddlcustomermap').multiselect({
                includeSelectAllOption: true
            });
        });
    </script>

    <script type="text/javascript">        
        $(function () {
            $('#ContentPlaceHolder1_ddlVendorMap').multiselect({
                includeSelectAllOption: true
            });
        });
    </script>

    <asp:UpdateProgress ID="UpdateProgress" runat="server">
        <ProgressTemplate>
            <asp:Image ID="Image1" ImageUrl="../images/loading123.gif" AlternateText="Processing"
                runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
        PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
        <ContentTemplate>
            <%--<div class="page_title">
                <span class="title_icon"><span class="computer_imac"></span></span>
                <h3>Miscellaneous Product Master</h3>
            </div>--%>

            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active" id="trBtn" runat="server">
                                <span class="h_icon_he list_images"></span>
                                <h6>Material Details</h6>
                                <div class="btn_30_light" style="float: right;" id="btnaddhide" runat="server">
                                    <span class="icon add_co"></span>
                                    <asp:Button ID="btnAdd" runat="server" Text="Add New Record" CssClass="btn_link"
                                        OnClick="btnAdd_Click" CausesValidation="false" />
                                </div>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td width="100" class="field_title">
                                                <asp:Label ID="lblproductowner" Text="Product Owner" runat="server"></asp:Label>
                                                <span class="req">*</span></td>
                                         

                                            <td width="282" class="field_input">
                                                <asp:DropDownList ID="ddlproductowner" runat="server" AppendDataBoundItems="true" Width="100" Height="28" class="chosen-select" data-placeholder="Choose a type" ValidationGroup="submitvalidation">
                                                    <%--<asp:ListItem Value="0" Text="-Select Owner-"></asp:ListItem>--%>
                                                    <asp:ListItem Value="M" Text="KKG"></asp:ListItem>
                                                    <%--<asp:ListItem Value="R" Text="Riya"></asp:ListItem>
                                                    <asp:ListItem Value="B" Text="Both"></asp:ListItem>--%>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="Reqproductowner" runat="server" ErrorMessage="Required!" EnableClientScript="true" ForeColor="Red"
                                                    ControlToValidate="ddlproductowner" ValidateEmptyText="false" SetFocusOnError="true" InitialValue="0" ValidationGroup="submitvalidation">
                                                </asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="100" class="field_title">
                                                <asp:Label ID="lblName" Text="Name" runat="server"></asp:Label></td>
                                            <td width="282" class="field_input">
                                                <asp:TextBox ID="txtName" runat="server" Style="text-transform: uppercase"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="CV_Name" runat="server" ErrorMessage="Product Name is required!" ForeColor="Red" Display="None" TabIndex ="0"
                                                    ControlToValidate="txtName" ValidateEmptyText="false" SetFocusOnError="true" ValidationGroup="submitvalidation"> </asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                                    TargetControlID="CV_Name" PopupPosition="Right" HighlightCssClass="errormassage"
                                                    WarningIconImageUrl="../images/050.png">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                            <td width="95" valign="top" class="field_title" style="padding-top: 18px;">
                                                <asp:Label ID="lblCode" Text="Code" runat="server" ></asp:Label><span class="req">*</span></td>
                                            <td class="field_input" valign="top">
                                                <asp:TextBox ID="txtCode" runat="server" Enabled="false" CssClass="mid"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="CV_Code" runat="server" ErrorMessage="Product Code is required!" ForeColor="Red" Display="None"
                                                    ControlToValidate="txtCode" ValidateEmptyText="false" SetFocusOnError="true" ValidationGroup="submitvalidation"> </asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                                                    TargetControlID="CV_Code" PopupPosition="Right" HighlightCssClass="errormassage"
                                                    WarningIconImageUrl="../images/050.png">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="100" class="field_title">
                                                <asp:Label ID="lblpitype" Text="Primary Item Type" runat="server"></asp:Label><span class="req">*</span></td>
                                            <td width="282" class="field_input">
                                                <asp:DropDownList ID="ddlpritype" runat="server" TabIndex ="0" AppendDataBoundItems="true" Width="250" Height="28" class="chosen-select" AutoPostBack="true"  OnSelectedIndexChanged="ddlpritype_SelectedIndexChanged" data-placeholder="Choose a type">
                                                </asp:DropDownList>&nbsp;<asp:Button ID="btnRefresh" runat="server" CssClass="h_icon arrow_refresh_co" ToolTip="Refresh" OnClick="btnRefresh_Click" CausesValidation="false" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required!" Font-Bold="true" ForeColor="Red"
                                                    ControlToValidate="ddlpritype" ValidateEmptyText="false" SetFocusOnError="true" InitialValue="0" ValidationGroup="submitvalidation"> </asp:RequiredFieldValidator>
                                            </td>
                                            </tr>
                                            <tr>
                                            <td width="95" valign="top" class="field_title" style="padding-top: 18px;">
                                                <asp:Label ID="lblCat" Text="Sub Item Type" runat="server"></asp:Label><span class="req">*</span></td>
                                            <td class="field_input" valign="top">
                                                <asp:DropDownList ID="ddlSbtype" runat="server" TabIndex ="0" AppendDataBoundItems="true" Width="250" Height="28" class="chosen-select" AutoPostBack="true" OnSelectedIndexChanged="ddlSbtype_SelectedIndexChanged" data-placeholder="Choose a Type">
                                                </asp:DropDownList>
                                                &nbsp;<td  id="td2" runat="server">
																<asp:Button ID="Button4" runat="server" CausesValidation="false" Enabled="true"
																	CssClass="h_icon add_co" ToolTip="Add Sub Item Type" OnClick="btnAddSubItem_Click" />
																<asp:Button ID="btnRefreshCategory" runat="server" CssClass="h_icon arrow_refresh_co" ToolTip="Refresh" OnClick="btnRefreshCategory_Click" CausesValidation="false" />
															</td>
                                                
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Required!" EnableClientScript="true" ForeColor="Red"
                                                    ControlToValidate="ddlSbtype" ValidateEmptyText="false" SetFocusOnError="true" InitialValue="0" ValidationGroup="submitvalidation">
                                                </asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                             <td class="field_title">
                                                  <asp:Label ID="lblHsnCode" Text="HsnCode" runat="server" >
                                             </asp:Label>&nbsp;&nbsp;&nbsp;
                                                  <span class="info_about" style="color:blue"> Hsn Code</span>
                                             </td>
                                            <td class="field_title">
                                                  <asp:Label ID="lblCgstPer" Text="Cgst Per" runat="server" >
                                             </asp:Label>&nbsp;&nbsp;&nbsp; 
                                                <span class="info_about_b" style="color:blue">Cgst Per</span>
                                             </td>
                                            <td class="field_title">
                                                  <asp:Label ID="lblSgstPer" Text="Sgst Per" runat="server" >
                                             </asp:Label>&nbsp;&nbsp;&nbsp;
                                                  <span class="info_about_b" style="color:blue">Sgst Per</span>
                                             </td>
                                            <td class="field_title">
                                                  <asp:Label ID="lblIgstPer" Text="Igst Per" runat="server" >
                                             </asp:Label>&nbsp;&nbsp;&nbsp;
                                                <span class="info_about_b" style="color:blue">Igst Per</span>
                                             </td>
                                            
                                        </tr>

                                         <tr>
                                            <td style="width:100px"  class="field_title">
                                                <asp:Label ID="Label6" Text="Brand" runat="server"></asp:Label><span class="req">*</span></td>
                                            <td width="282" class="field_input">
                                                <asp:DropDownList ID="ddlbrand" runat="server" TabIndex ="0" AppendDataBoundItems="true" Width="250" Height="28" class="chosen-select" AutoPostBack="true"  OnSelectedIndexChanged="ddlbrand_SelectedIndexChanged" data-placeholder="Choose a type">
                                                </asp:DropDownList>&nbsp;<span class="label_intro">	
															</td>
                                             <td  id="td4" runat="server">
                                                 <asp:Button ID="Button3" runat="server" CausesValidation="false" Enabled="true"
																	CssClass="h_icon add_co" ToolTip="Add Type" OnClick="btnAddBrand_Click" />
                                                <asp:Button ID="RefreshBrand" runat="server" CssClass="h_icon arrow_refresh_co" ToolTip="Refresh" OnClick="btnRefreshBrand_Click" CausesValidation="false" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Required!" Font-Bold="true" ForeColor="Red"
                                                    ControlToValidate="ddlbrand" ValidateEmptyText="false" SetFocusOnError="true" InitialValue="0" ValidationGroup="submitvalidation"> </asp:RequiredFieldValidator>
                                            </td>
                                           
                                            </tr>
                                         
                                           <%-- <td width="282" class="field_input">
                                                <asp:DropDownList ID="ddlsw" runat="server" TabIndex ="0" AppendDataBoundItems="true" Width="250" Height="28" class="chosen-select" AutoPostBack="true"  OnSelectedIndexChanged="ddlsw_SelectedIndexChanged" data-placeholder="Choose a type">
                                                </asp:DropDownList>&nbsp;<span class="label_intro">	
															</td>--%>

                                             <%--<td class="field_title">
                                                <asp:Label ID="Label8" Text="Unit Capacity" runat="server"></asp:Label><span class="req">*</span></td>
                                            <td class="field_input">
                                                <asp:TextBox ID="TextBox1" runat="server" Style="width: 250px;"
                                                    MaxLength="4" onkeypress="return validateFloatKeyPress(this,event);" AutoPostBack="true" OnTextChanged="txtUValue_SelectedIndexChanged"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Unit value is required!" ForeColor="Red" Display="None"
                                                    ControlToValidate="txtUValue" ValidateEmptyText="false" SetFocusOnError="true" ValidationGroup="submitvalidation"> </asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server"
                                                    TargetControlID="RequiredFieldValidator5" PopupPosition="Right" HighlightCssClass="errormassage"
                                                    WarningIconImageUrl="../images/050.png">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>--%>


                                            <%-- <td  id="td3" runat="server">
                                                <asp:Button ID="Button1" runat="server" CausesValidation="false" Enabled="true"
																	CssClass="h_icon add_co" ToolTip="Add Type" OnClick="btnAddsize_Click" />
                                                <asp:Button ID="btnRefreshsw" runat="server" CssClass="h_icon arrow_refresh_co" ToolTip="Refresh" OnClick="btnRefreshsw_Click" CausesValidation="false" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Required!" Font-Bold="true" ForeColor="Red"
                                                    ControlToValidate="ddlsw" ValidateEmptyText="false" SetFocusOnError="true" InitialValue="0" ValidationGroup="submitvalidation"> </asp:RequiredFieldValidator>
                                            </td>--%>
                                           
                                            
                                         <tr>
                                            <td class="field_title">
                                                <asp:Label ID="Label1" Text="TYPE" runat="server"></asp:Label><span class="req">*</span></td>
                                            <td class="field_input">
                                                <asp:DropDownList ID="ddltype" runat="server"  AppendDataBoundItems="true" Width="250" Height="28" class="chosen-select" AutoPostBack="true" OnSelectedIndexChanged="ddltype_SelectedIndexChanged" data-placeholder="Choose a UOM">
                                                </asp:DropDownList>&nbsp;<span class="label_intro">	
															</td>
															<td  id="tdaddCR" runat="server">
																<asp:Button ID="btnAddType" runat="server" CausesValidation="false" Enabled="true"
																	CssClass="h_icon add_co" ToolTip="Add Type" OnClick="btnAddType_Click" />
																<asp:Button ID="btnRefreshTYPE" runat="server" CausesValidation="false" CssClass="h_icon arrow_refresh_co"
																	Enabled="true" OnClick="btnRefreshTYPE_Click" ToolTip="Refresh" />
															
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Type is Required!" Font-Bold="true" ForeColor="Red"
                                                    ControlToValidate="ddltype" ValidateEmptyText="false" SetFocusOnError="true" InitialValue="0" ValidationGroup="submitvalidation"> </asp:RequiredFieldValidator>
                                                <%--<ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server"
                                                    TargetControlID="RequiredFieldValidator4" PopupPosition="Right" HighlightCssClass="errormassage"
                                                    WarningIconImageUrl="../images/050.png">
                                                </ajaxToolkit:ValidatorCalloutExtender>--%>
                                                                
                                            </td>
                                             </tr>
                                             <tr>
                                            <td class="field_title">
                                                <asp:Label ID="Label4" Text="COLOR" runat="server"></asp:Label><span class="req">*</span></td>&nbsp;<td class="field_input" runat="server"> 
                                               <asp:DropDownList ID="ddlcolor" runat="server" AppendDataBoundItems="true" Width="250" Height="28" class="chosen-select" AutoPostBack="true" OnSelectedIndexChanged="ddlcolor_SelectedIndexChanged" data-placeholder="Choose a UOM">
                                                </asp:DropDownList>&nbsp;
                                                    </td>
                                                    <td  id="td1" runat="server">
																<asp:Button ID="btnAddColor" runat="server" CausesValidation="false" Enabled="true"
																	CssClass="h_icon add_co" ToolTip="Add Color" OnClick="btnAddColor_Click" />
																<asp:Button ID="btnRefreshcolor" runat="server" CausesValidation="false" CssClass="h_icon arrow_refresh_co"
																	Enabled="true" OnClick="btnRefreshcolor_Click" ToolTip="Refresh" />
															
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Color is required!" ForeColor="Red" Display="None"
                                                    ControlToValidate="ddlcolor" ValidateEmptyText="false" SetFocusOnError="true" InitialValue="0" ValidationGroup="submitvalidation"> </asp:RequiredFieldValidator>
                                                <%--<ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server"
                                                    TargetControlID="RequiredFieldValidator5" PopupPosition="Right" HighlightCssClass="errormassage"
                                                    WarningIconImageUrl="../images/050.png">
                                                </ajaxToolkit:ValidatorCalloutExtender>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="100" class="field_title">
                                                <asp:Label ID="Label7" Text="SIZE" runat="server"></asp:Label><span class="req">*</span></td>
                                             <td class="field_input">
                                                <asp:TextBox ID="txtSize" runat="server" Style="width: 250px;"
                                                    MaxLength="8" AutoPostBack="true" OnTextChanged="txtSize_SelectedIndexChanged"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Size is required!" ForeColor="Red" Display="None"
                                                    ControlToValidate="txtSize" ValidateEmptyText="false" SetFocusOnError="true" ValidationGroup="submitvalidation"> </asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server"
                                                    TargetControlID="RequiredFieldValidator5" PopupPosition="Right" HighlightCssClass="errormassage"
                                                    WarningIconImageUrl="../images/050.png">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                               
                                             </tr>
                                        <tr>
                                            <td class="field_title">
                                                <asp:Label ID="lblUOM" Text="UOM" runat="server"></asp:Label><span class="req">*</span></td>
                                            <td class="field_input">
                                                <asp:DropDownList ID="ddlUOM" runat="server" AppendDataBoundItems="true" Width="250" Height="28" class="chosen-select" AutoPostBack="true" OnSelectedIndexChanged="ddlUOM_SelectedIndexChanged" data-placeholder="Choose a UOM">
                                                </asp:DropDownList>&nbsp;<asp:Button ID="btnRefreshUOM" runat="server" CssClass="h_icon arrow_refresh_co" ToolTip="Refresh" OnClick="btnRefreshUOM_Click" CausesValidation="false" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Required!" ForeColor="Red"
                                                    ControlToValidate="ddlUOM" ValidateEmptyText="false" SetFocusOnError="true" InitialValue="0" ValidationGroup="submitvalidation"> </asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server"
                                                    TargetControlID="RequiredFieldValidator4" PopupPosition="Right" HighlightCssClass="errormassage"
                                                    WarningIconImageUrl="../images/050.png">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                            <td class="field_title">
                                                <asp:Label ID="lblUValue" Text="Unit Capacity" runat="server"></asp:Label><span class="req">*</span></td>
                                            <td class="field_input">
                                                <asp:TextBox ID="txtUValue" runat="server" Style="width: 250px;"
                                                    MaxLength="4" onkeypress="return validateFloatKeyPress(this,event);" AutoPostBack="true" OnTextChanged="txtUValue_SelectedIndexChanged"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Unit value is required!" ForeColor="Red" Display="None"
                                                    ControlToValidate="txtUValue" ValidateEmptyText="false" SetFocusOnError="true" ValidationGroup="submitvalidation"> </asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                                    TargetControlID="RequiredFieldValidator5" PopupPosition="Right" HighlightCssClass="errormassage"
                                                    WarningIconImageUrl="../images/050.png">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                        </tr>


                                    </table>
                                  
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td width="100" class="field_title">
                                                <asp:Label ID="lblMRP" Text="MRP" runat="server"></asp:Label></td>
                                            <td width="255" class="field_input">
                                                <asp:TextBox ID="txtMRP" runat="server" Style="width: 250px;" onkeypress="return validateFloatKeyPress(this,event);"></asp:TextBox>

                                            </td>
                                            <td width="140" class="field_title">
                                                <asp:Label ID="lblMinstooklevel" Text="Re-Order Level" runat="server"></asp:Label>
                                            </td>
                                            <td class="field_input">
                                                <asp:TextBox ID="txtMinstooklevel" runat="server" Style="width: 250px;" MaxLength="30" onkeypress="return validateFloatKeyPress(this,event);"></asp:TextBox>

                                            </td>
                                        </tr>

                                        <tr>
                                            <td width="100" class="field_title">
                                                <asp:Label ID="lbl" Text="FACTORY MAP" runat="server"></asp:Label>
                                                <span class="req">*</span>
                                            </td>
                                            <td class="field_input">
                                                <%--<asp:ListBox ID="ddlfactorymap" runat="server" SelectionMode="Multiple" TabIndex="4"
                                                    Width="100" AppendDataBoundItems="True" name="options[]" multiple="multiple"></asp:ListBox>--%>

                                                <asp:DropDownList ID="ddlfactorymap" runat="server" AppendDataBoundItems="true" Width="200" Height="28" class="chosen-select"  data-placeholder="Choose a type" ValidationGroup="submitvalidation">                                                                                            
                                                </asp:DropDownList>     

                                            </td>

                                            <td width="100" class="field_title">
                                                <asp:Label ID="lblcustomer" Text="CUSTOMER MAP" runat="server"></asp:Label>
                                                <span class="req"></span>
                                            </td>
                                            <td class="field_input">
                                                <asp:ListBox ID="ddlcustomermap" runat="server" SelectionMode="Multiple" TabIndex="4"
                                                    Width="100" AppendDataBoundItems="True"></asp:ListBox>
                                                <%--<asp:CustomValidator ID="cvddlcustomermap" runat="server" ValidateEmptyText="true"
                                                    ControlToValidate="ddlcustomermap" ValidationGroup="submitvalidation" ErrorMessage="Required!"
                                                    Display="Dynamic" ClientValidationFunction="ValidateListBox_Customer" ForeColor="Red"></asp:CustomValidator>--%>
                                            </td>

                                            <td width="100" class="field_title">
                                                <asp:Label ID="Label3" Text="VENDOR MAP" runat="server"></asp:Label>
                                                <span class="req"></span>
                                            </td>
                                            <td class="field_input">
                                                <asp:ListBox ID="ddlVendorMap" runat="server" SelectionMode="Multiple" TabIndex="4"
                                                    Width="100" AppendDataBoundItems="True"></asp:ListBox>
                                                <%--<asp:CustomValidator ID="cvddlVendorMap" runat="server" ValidateEmptyText="true"
                                                                ControlToValidate="ddlVendorMap" ValidationGroup="submitvalidation" ErrorMessage="Required!"
                                                                Display="Dynamic" ClientValidationFunction="ValidateListBox_VendorMap" ForeColor="Red"></asp:CustomValidator>--%>

                                            </td>
                                        </tr>

                                        <tr>
                                            <td width="100" class="field_title">
                                                <asp:TextBox ID="txtUnitvalue" runat="server" CssClass="large" ValidationGroup="Mapping" Width="85px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="CV_txtUnitvalue" runat="server"
                                                    ControlToValidate="txtUnitvalue" Display="None"
                                                    ErrorMessage="Unit value is required!" SetFocusOnError="true"
                                                    ValidateEmptyText="false" ValidationGroup="submitvalidation"> </asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender8"
                                                    runat="server" HighlightCssClass="errormassage" PopupPosition="BottomLeft"
                                                    TargetControlID="CV_txtUnitvalue" WarningIconImageUrl="../images/050.png">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                <br />
                                                <span class=" label_intro">Unit&nbsp;Capacity <span class="req">*</span></span>
                                            </td>
                                            <td width="223" class="field_input">
                                                <asp:DropDownList ID="ddlPackigsize1" runat="server"
                                                    AppendDataBoundItems="true" class="chosen-select"
                                                    data-placeholder="Choose a Packing Size" Style="width: 220px;">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="CV_ddlPackigsize1" runat="server"
                                                    ControlToValidate="ddlPackigsize1" Display="None"
                                                    ErrorMessage="PackigSize From is required!" InitialValue="0"
                                                    SetFocusOnError="true" ValidateEmptyText="false" ValidationGroup="submitvalidation"> </asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender9"
                                                    runat="server" HighlightCssClass="errormassage" PopupPosition="BottomLeft"
                                                    TargetControlID="CV_ddlPackigsize1" WarningIconImageUrl="../images/050.png">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                <span class=" label_intro">Packing Size From <span class="req">*</span></span>
                                            </td>
                                            <td width="53" valign="top" style="padding-top: 15px;" class="field_title">Make 1</td>
                                            <td width="230" class="field_input">
                                                <asp:DropDownList ID="ddlPackigsize" runat="server" AppendDataBoundItems="true"
                                                    class="chosen-select" Style="width: 220px;"
                                                    data-placeholder="Choose a Packing Size"
                                                    ValidationGroup="Mapping">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="CV_ddlPackigsize" runat="server" Display="None" ErrorMessage="PackigSize To is required!"
                                                    ControlToValidate="ddlPackigsize" ValidateEmptyText="false" SetFocusOnError="true" InitialValue="0" ValidationGroup="submitvalidation">
                                                </asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server"
                                                    TargetControlID="CV_ddlPackigsize" PopupPosition="BottomLeft" HighlightCssClass="errormassage"
                                                    WarningIconImageUrl="../images/050.png">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                <br />
                                                <span class=" label_intro">Packing Size To <span class="req">*</span></span>
                                            </td>
                                              <td width="50" class="innerfield_title">
                                                                            <asp:Label ID="Label40" Text="StoreLocation" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td width="80">
                                                                            <asp:DropDownList ID="ddlstorelocation" runat="server" Width="200px" AutoPostBack="true"
                                                                                AppendDataBoundItems="True" class="chosen-select" data-placeholder="Select Location">
                                                                            </asp:DropDownList>
                                                                        </td>
                                        </tr>

                                        <tr>
                                            <td class="field_title">
                                                <asp:Label ID="lblAssessablepercentage" Text="Assessable" runat="server" Visible="false"></asp:Label>&nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td class="field_input">
                                                <asp:TextBox ID="txtAssessablepercentage" runat="server" Text="0" Style="width: 90px;" MaxLength="30"
                                                    onkeypress="return validateFloatKeyPress(this,event);" Visible="false"></asp:TextBox>
                                                <asp:Label ID="Label5" runat="server" Text="&nbsp;%" Font-Bold="True" Visible="false"></asp:Label>

                                                <%--  <asp:RequiredFieldValidator ID="CV_txtAssessablepercentage" runat="server" ErrorMessage="Assessable Percentage is required!"
                                                    ControlToValidate="txtAssessablepercentage" ValidateEmptyText="false" Display="None" SetFocusOnError="true"  ValidationGroup="submitvalidation" > </asp:RequiredFieldValidator>--%>
                                                <%--  <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server"
                                                    TargetControlID="CV_txtAssessablepercentage" PopupPosition="Right" HighlightCssClass="errormassage"
                                                    WarningIconImageUrl="../images/050.png">
                                                </ajaxToolkit:ValidatorCalloutExtender>--%>
                                            </td>
                                            <td class="field_title">
                                                <asp:Label ID="lblRefundable" Text="Returnable" runat="server" Visible="false"></asp:Label></td>
                                            <td class="field_input" colspan="3">
                                                <asp:CheckBox ID="chkRefundable" runat="server" Visible="false" Text=" "  /></td>
                                        </tr>
                                        <tr>
                                            <td class="field_title">
                                                <asp:Label ID="lblActive" Text="Active" runat="server"></asp:Label></td>
                                            <td class="field_input" colspan="3">
                                                <asp:CheckBox ID="ChkActive" runat="server" Text=" " /></td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td style="padding: 8px 0;" colspan="3">
                                                <div class="btn_24_blue">
                                                    <span class="icon disk_co"></span>
                                                    <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="btn_link" ValidationGroup="submitvalidation" OnClick="btnSubmit_Click" />
                                                </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                            <div class="btn_24_blue">
                                                <span class="icon cross_octagon_co"></span>
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn_link" OnClick="btnCancel_Click" CausesValidation="false" />
                                            </div>
                                                <asp:HiddenField ID="Hdn_Fld" runat="server" />
                                                <asp:HiddenField ID="Hdn_Supplied" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>

                                <asp:Panel ID="PnlDepot" runat="server">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                        <tr>
                                            <td width="100" class="field_title">
                                                <asp:Label ID="Label2" Text="NAME" runat="server"></asp:Label><span class="req">*</span></td>
                                            <td class="field_input">
                                                <asp:TextBox ID="txtproduct" runat="server" CssClass="mid" Enabled="false" ></asp:TextBox></td>
                                        </tr>
                                        <tr runat="server" id="TrDepot">
                                            <td colspan="2" class="field_input" style="padding-left: 10px;">
                                                <fieldset>
                                                    <legend>Depot Mapping Details</legend>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td valign="top">
                                                                <cc1:Grid ID="gvFactory" runat="server" FolderStyle="../GridStyles/premiere_blue"
                                                                    AutoGenerateColumns="false" AllowSorting="false" AllowPageSizeSelection="false"
                                                                    AllowPaging="false" AllowFiltering="false" Serialize="true" AllowAddingRecords="false" PageSize="500">
                                                                    <FilteringSettings InitialState="Visible" />
                                                                    <Columns>
                                                                        <cc1:Column ID="Column5" DataField="VENDORNAME" HeaderText="FACTORY MAPPING" runat="server"
                                                                            Width="250" Wrap="true">
                                                                            <FilterOptions>
                                                                                <cc1:FilterOption Type="NoFilter" />
                                                                                <cc1:FilterOption Type="Contains" />
                                                                            </FilterOptions>
                                                                        </cc1:Column>
                                                                        <cc1:Column ID="Column9" DataField="VENDORID" ReadOnly="true" HeaderText="ID" runat="server"
                                                                            Width="40" Wrap="true">
                                                                            <TemplateSettings TemplateId="CheckTemplateFactory" HeaderTemplateId="HeaderTemplateFactory" />
                                                                        </cc1:Column>
                                                                    </Columns>
                                                                    <FilteringSettings MatchingType="AnyFilter" />
                                                                    <Templates>
                                                                        <cc1:GridTemplate runat="server" ID="CheckTemplateFactory">
                                                                            <Template>
                                                                                <asp:HiddenField runat="server" ID="hdnFactoryName" Value='<%# Container.DataItem["VENDORNAME"] %>' />
                                                                                <asp:CheckBox ID="ChkIDFactory" runat="server" Text=" " ToolTip="<%# Container.Value %>" />
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
                                    </table>
                                </asp:Panel>

                                <asp:Panel ID="pnlDisplay" runat="server">
                                    <table>
                                        <tr>
                                            <ul id="search_box">
                                                <li>
                                                    <input name="" id="FilterTextBox" type="text" class="search_input" placeholder="Enter text to search the whole grid"
                                                        onkeyup="FilterTextBox_KeyUp();">
                                                </li>
                                                <li>
                                                    <input name="" id="OboutButton2" type="submit" value="" class="search_btn" onclick="return performSearch();">
                                                </li>
                                                <li>&nbsp;&nbsp;&nbsp;
                                        
                                                </li>
                                                <li>

                                                    <div class="btn_24_blue">
                                                        <span class="icon doc_excel_table_co"></span>
                                                        <asp:Button ID="btnExport" runat="server" Text="Export Excel" CssClass="btn_link"
                                                            CausesValidation="false" OnClick="btnExport_Click" />
                                                    </div>

                                                </li>
                                            </ul>
                                            <td class="field_input"></td>
                                        </tr>
                                    </table>
                                    <div class="gridcontent">
                                        <cc1:Grid ID="gvMiscellaneousProduct" runat="server" CallbackMode="true"
                                            Serialize="true" FolderStyle="../GridStyles/premiere_blue"
                                            AutoGenerateColumns="false" OnDeleteCommand="DeleteRecord" EnableRecordHover="true"
                                            AllowAddingRecords="false" AllowFiltering="true"
                                            AllowMultiRecordSelection="false" AllowPageSizeSelection="true"
                                            OnRowDataBound="gvMiscellaneousProduct_RowDataBound">
                                            <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" OnClientDelete="OnDelete" />
                                            <FilteringSettings InitialState="Visible" />
                                            <Columns>
                                                <cc1:Column DataField="ID" ReadOnly="true" HeaderText="ID" runat="server" Visible="false" />
                                                <cc1:Column DataField="PRODUCTOWNER" HeaderText="OWNER" runat="server" Width="80" Wrap="true">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="EqualTo" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="DoesNotContain" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>

                                                <cc1:Column DataField="CODE" HeaderText="CODE" runat="server" Width="100" Wrap="true">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="EqualTo" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="DoesNotContain" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>

                                                <cc1:Column DataField="DIVNAME" HeaderText="PRIMARY ITEM" runat="server" Width="100" Wrap="true">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="EqualTo" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="DoesNotContain" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>

                                                <cc1:Column DataField="CATNAME" HeaderText="SUB ITEM" runat="server" Width="110" Wrap="true">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="EqualTo" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="DoesNotContain" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>

                                                <cc1:Column DataField="NAME" HeaderText="NAME" runat="server" Width="200" Wrap="true">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="EqualTo" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="DoesNotContain" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column2" DataField="DIVID" HeaderText="DIVID" runat="server" Width="300" Wrap="true" Visible="false">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="EqualTo" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="DoesNotContain" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column1" DataField="MRP" HeaderText="MRP" runat="server" Width="50"></cc1:Column>
                                                <cc1:Column DataField="ACTIVE" HeaderText="STATUS" runat="server" Width="70">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="EqualTo" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="DoesNotContain" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>

                                                <cc1:Column DataField="BRANCHNAME" HeaderText="FACTORY MAP" runat="server" Width="155">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="EqualTo" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="DoesNotContain" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>

                                                <cc1:Column DataField="VENDORNAME" HeaderText="VENDOR MAP" runat="server" Width="140">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="EqualTo" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="DoesNotContain" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>

                                                <cc1:Column DataField="CUSTOMERNAME" HeaderText="CUSTOMER MAP" runat="server" Width="130">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="EqualTo" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="DoesNotContain" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>

                                                <cc1:Column DataField="UNITMAP" HeaderText="UNIT MAP" runat="server" Width="80">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="EqualTo" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="DoesNotContain" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>

                                                <cc1:Column AllowEdit="true" AllowDelete="true" HeaderText="EDIT" runat="server"
                                                    Width="50">
                                                    <TemplateSettings TemplateId="editBtnTemplate" />
                                                </cc1:Column>
                                                <cc1:Column HeaderText="DELETE" AllowEdit="false" AllowDelete="true" Width="70" Visible="false">
                                                    <TemplateSettings TemplateId="deleteBtnTemplate" />
                                                </cc1:Column>
                                                <cc1:Column HeaderText="Vendor Mapping " AllowEdit="false" AllowDelete="true" Width="70" Wrap="true" Visible="false">
                                                    <TemplateSettings TemplateId="MappingTemplatePackSize" />
                                                </cc1:Column>
                                                <cc1:Column HeaderText="Unit Mapping " AllowEdit="false" AllowDelete="true" Width="70" Wrap="true" Visible="false">
                                                    <TemplateSettings TemplateId="MappingTemplatePackSize1" />
                                                </cc1:Column>
                                                <cc1:Column HeaderText="Factory / TPU Mapping" AllowEdit="false" AllowDelete="true"
                                                    Align="center" Width="90" Wrap="true" Visible="false">
                                                    <TemplateSettings TemplateId="MappingTemplateDepot" />
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
                                                        <a href="javascript: //" class="action-icons c-delete" title="Delete" onclick="gvMiscellaneousProduct.delete_record(this)"></a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>

                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="MappingTemplatePackSize">
                                                    <Template>

                                                        <a href="javascript: //" class="h_icon map" title="Vendor Mapping" onclick="openBSMappingNew('<%# Container.DataItem["NAME"] %>','<%# Container.DataItem["ID"] %>','<%# Container.DataItem["DIVID"] %>')" />
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="MappingTemplatePackSize1">
                                                    <Template>
                                                        <a href="javascript: //" class="h_icon map" title="Unit Mapping" onclick="openPackSizeMappingNew('<%# Container.DataItem["NAME"] %>','<%# Container.DataItem["ID"] %>')" />
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>

                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="MappingTemplateDepot">
                                                    <Template>
                                                        <a href="javascript: //" class="filter_btn electricity_input" title="Factory / TPU Mapping"
                                                            onclick="OpenDepotMapping('<%# Container.DataItem["PRODUCTALIAS"] %>','<%# Container.DataItem["ID"] %>')" />
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>

                                            <%--<ScrollingSettings ScrollWidth="100%" ScrollHeight="290" />--%>
                                        </cc1:Grid>

                                        <asp:HiddenField runat="server" ID="hdnPid" />
                                        <asp:Button ID="btngridedit" runat="server" Text="GridEdit" Style="display: none"
                                            OnClick="btngridedit_Click" CausesValidation="false" />
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                    <cc1:MessageBox ID="MessageBox1" runat="server" />

                    <span class="clear"></span>
                </div>
                <span class="clear"></span>

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
                    var searchTimeout = null;
                    function FilterTextBox_KeyUp() {
                        searchTimeout = window.setTimeout(performSearch, 500);
                    }

                    function performSearch() {
                        var searchValue = document.getElementById('FilterTextBox').value;
                        if (searchValue == FilterTextBox.WatermarkText) {
                            searchValue = '';
                        }
                        gvMiscellaneousProduct.addFilterCriteria('CODE', OboutGridFilterCriteria.Contains, searchValue);
                        gvMiscellaneousProduct.addFilterCriteria('NAME', OboutGridFilterCriteria.Contains, searchValue);
                        gvMiscellaneousProduct.addFilterCriteria('PRODUCTOWNER', OboutGridFilterCriteria.Contains, searchValue);
                        gvMiscellaneousProduct.addFilterCriteria('DIVNAME', OboutGridFilterCriteria.Contains, searchValue);
                        gvMiscellaneousProduct.addFilterCriteria('CATNAME', OboutGridFilterCriteria.Contains, searchValue);
                        gvMiscellaneousProduct.addFilterCriteria('ACTIVE', OboutGridFilterCriteria.Contains, searchValue);
                        gvMiscellaneousProduct.addFilterCriteria('BRANCHNAME', OboutGridFilterCriteria.Contains, searchValue);
                        gvMiscellaneousProduct.addFilterCriteria('VENDORNAME', OboutGridFilterCriteria.Contains, searchValue);
                        gvMiscellaneousProduct.executeFilter();
                        searchTimeout = null;
                        return false;
                    }
                </script>
                <script type="text/javascript">
                    function CallServerMethod(oLink) {
                        debugger;
                        document.getElementById("<%=hdnPid.ClientID %>").value = '';
                        var iRecordIndex = oLink.id.toString().replace("btnGridEdit_", "");
                        document.getElementById("<%=Hdn_Fld.ClientID %>").value = gvMiscellaneousProduct.Rows[iRecordIndex].Cells[0].Value;
                        document.getElementById("<%=btngridedit.ClientID %>").click();
                    }
                </script>
                <script type="text/javascript">
                    function validateFloatKeyPress(el, evt) {
                        var charCode = (evt.which) ? evt.which : event.keyCode;

                        if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57 )) {
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

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>