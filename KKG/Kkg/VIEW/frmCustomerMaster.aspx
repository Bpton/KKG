<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmCustomerMaster.aspx.cs" Inherits="VIEW_frmCustomerMaster" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls"
	TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
	<script type="text/javascript">
		function Confirm() {
			debugger;
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
		function ValidateListBoxddlsearchbs(sender, args) {
			var options = document.getElementById("<%=ddlsearchbs.ClientID%>").options;
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
		function ValidateListBoxddldepots(sender, args) {
			var options = document.getElementById("<%=ddldepots.ClientID%>").options;
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
		function ValidateListBoxddlcustype(sender, args) {
			var options = document.getElementById("<%=ddlcustype.ClientID%>").options;
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
		function ValidateListBoxddlaccountsgroup(sender, args) {
			var options = document.getElementById("<%=ddlaccountsgroup.ClientID%>").options;
			for (var i = 0; i < options.length; i++) {
				if (options[i].selected == true) {
					args.IsValid = true;
					return;
				}
			}
			args.IsValid = false;
		}
	</script>
	<script type="text/javascript" language="javascript">
		function CheckAllheadervou(Checkbox) {
			var GridVwHeaderCheckbox = document.getElementById("<%=gvCustomerDepotMapping.ClientID %>");
			var lbltotalcount = document.getElementById("<%=lbltotalcount.ClientID %>");
			var totalcount = 0;
			if (Checkbox.checked) {
				for (i = 1; i < GridVwHeaderCheckbox.rows.length; i++) {
					GridVwHeaderCheckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
					/*GridVwHeaderCheckbox.rows[i].style.color = '#389638';*/
					GridVwHeaderCheckbox.rows[i].cells[0].style.border = '1px solid #fff';
					totalcount = totalcount + 1;
				}
			}
			else {
				for (i = 1; i < GridVwHeaderCheckbox.rows.length; i++) {
					GridVwHeaderCheckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = false;
					/*GridVwHeaderCheckbox.rows[i].style.color = '#4B555E';*/
					GridVwHeaderCheckbox.rows[i].cells[0].style.border = '0px solid gray';
				}
			}
			if (totalcount > 0) {
				lbltotalcount.style.display = "";
				lbltotalcount.innerHTML = 'You have selected : ' + totalcount + ' invoices.';
			}
			else {
				lbltotalcount.style.display = "none";
			}
		}
	</script>

	<script type="text/javascript">
		function setrowcolorvou(chb) {
			if (chb.checked) {
				/*chb.parentNode.parentNode.style.color = '#389638';*/
				chb.parentNode.parentNode.style.border = '1px solid #fff';
			}
			else {
				/*chb.parentNode.parentNode.style.color = '#4B555E';*/
				chb.parentNode.parentNode.style.border = '0px solid gray';
			}

			var GridVwHeaderCheckbox = document.getElementById("<%=gvCustomerDepotMapping.ClientID %>");
			var lbltotalcount = document.getElementById("<%=lbltotalcount.ClientID %>");
			var totalcount = 0;

			for (i = 1; i < GridVwHeaderCheckbox.rows.length; i++) {
				if (GridVwHeaderCheckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked == true) {
					totalcount = totalcount + 1;
				}
			}
			if (totalcount > 0) {
				lbltotalcount.style.display = "";
				lbltotalcount.innerHTML = 'You have selected : ' + totalcount + ' invoices.';
			}
			else {
				lbltotalcount.style.display = "none";
			}
		}
	</script>
	<script type="text/javascript">
		function checkadds(Checkbox) {

			var a = document.getElementById('ContentPlaceHolder1_trMarginPer');
			if (Checkbox.checked) {

				a.style.display = "";

			}
			else {
				a.style.display = "none";
			}
		}
	</script>

	<script type="text/javascript">

		function chkactive(Checkbox) {
			debugger;
			var a = document.getElementById('ContentPlaceHolder1_trinactivedate');
			if (!Checkbox.checked) {
				var retVal = confirm("Do you want to inactive ?");
				if (retVal == true) {
					a.style.display = "";
					return true;
				}
				else {
					document.getElementById('ContentPlaceHolder1_chkActive').checked = true;
				}
			}
			else

				a.style.display = "none";

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
                           var txtPanNO = $('#<%=txtPanNO.ClientID%>').val();
                           var txtgstnoL = txtPanNO.length;  
                           calchkgst();
                           if (txtgstnoL == 10) { }
                           else {

                                alert('PAN No. must be 10 digit. Invalid PAN No.');
                               $('#<%=txtPanNO.ClientID%>').val('');
                                    return false;
                           }
                           for (var i = 0; i < txtgstnoL; i++) {  
                                if (i >=0 && i<=4) {
                                    var n1 = txtPanNO.charCodeAt(i);                                   
                                if ((n1 >= 65 && n1 <= 90) || (n1 >= 97 && n1 <= 122)) {
                                }
                                else {
                                     alert('1st 5 digit must be string.');
                                    $('#<%=txtPanNO.ClientID%>').val('');
                                    return false;
                                }
                               }

                                if (i >4 && i<=8) {
                                    var n = txtPanNO.charCodeAt(i);
                                if (n < 48 || n > 57) {
                                     alert('6th to 9th must be number.');
                                    $('#<%=txtPanNO.ClientID%>').val('');
                                     return false;
                                }
                            }
                            
                             if (i == 9) {
                                var n2 = txtPanNO.charCodeAt(i);
                                 if ((n2 >= 65 && n2 <= 90) || (n2 >= 97 && n2 <= 122)) {
                                 }
                                 else {
                                      alert('10th digit must be string.');
                                     $('#<%=txtPanNO.ClientID%>').val('');
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
			<asp:Image ID="Image1" ImageUrl="../images/loading123.gif" AlternateText="Processing"
				runat="server" />
		</ProgressTemplate>
	</asp:UpdateProgress>
	<ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
		PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
	<asp:UpdatePanel ID="UpdatePanel1" runat="server">
		<ContentTemplate>
		</ContentTemplate>
	</asp:UpdatePanel>
	<asp:UpdatePanel ID="UpdatePanel2" runat="server">
		<Triggers>
			<asp:PostBackTrigger ControlID="btngeneraltemp" />
		</Triggers>
		<ContentTemplate>
			<div id="contentarea">
				<div class="grid_container">
					<div class="grid_12">
						<div class="widget_wrap">
							<div class="widget_top active" id="trBtn" runat="server">
								<span class="h_icon_he list_images"></span>
								<h6>Customer Master Details</h6>
								<div class="btn_30_light" style="float: right;" id="btnaddhide" runat="server">
									<span class="icon add_co"></span>
									<asp:Button ID="btnAddCustomer" runat="server" Text="Add New Record" CssClass="btn_link"
										CausesValidation="false" OnClick="btnAddCustomer_Click" />
								</div>
							</div>
							<div class="widget_content">
								<asp:Panel ID="pnlAdd" runat="server">
									<table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
										<tr>
											<td class="field_title" width="90">
												<asp:Label ID="Label26" Text="REG CUSTOMER TYPE" runat="server"></asp:Label>
											</td>
											<td class="field_input">
												<table width="50%" cellpadding="0" cellspacing="0" border="0">
													<tr>
														<td width="120">
															<asp:DropDownList ID="ddlcompanytype" Width="120" runat="server" class="chosen-select"
																data-placeholder="Select Company" AppendDataBoundItems="True">
															</asp:DropDownList>
														</td>
														<td width="50" class="field_title">
															<asp:Label ID="Label10" Text="Depot" runat="server"></asp:Label>
															<span class="req">*</span>
														</td>
														<td class="field_input">
															<asp:DropDownList ID="ddlDepot" Width="120" runat="server" class="chosen-select" AutoPostBack="true"
																data-placeholder="Choose Depot" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlDepot_SelectedIndexChanged"
																ValidationGroup="ADD">
															</asp:DropDownList>
															<asp:RequiredFieldValidator ID="RequiredFieldValidatorGroup" runat="server" ControlToValidate="ddlDepot" ValidateEmptyText="false"
																SetFocusOnError="true" ErrorMessage="*" ValidationGroup="Show" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
														</td>

														<td width="60" class="innerfield_title">
															<asp:Label ID="lblCode" Text="Code" runat="server"></asp:Label>

														</td>
														<td width="135">
															<asp:TextBox ID="txtcode" runat="server" AutoCompleteType="Disabled" Enabled="false"
																Width="120"> </asp:TextBox>

														</td>
														<td width="60">&nbsp;
														</td>

													</tr>
												</table>
											</td>
										</tr>
										<tr>
											<td class="field_title" width="90">
												<asp:Label ID="lblName" Text="Customer Name" runat="server"></asp:Label>
												<span class="req">*</span>
											</td>
											<td class="field_input">
												<table width="100%" cellpadding="0" cellspacing="0" border="0">
													<tr>
														<td width="200">
															<asp:TextBox ID="txtName" runat="server" ValidationGroup="Submit" AutoCompleteType="Disabled"
																onfocus="disableautocompletion(this.id);" Width="200" onkeyup="printname()"> </asp:TextBox>
															<asp:RequiredFieldValidator ID="CV_Name" runat="server" ValidationGroup="Submit"
																Display="None" ErrorMessage="Customer Name is required!" ControlToValidate="txtName"
																ValidateEmptyText="false" SetFocusOnError="true"> </asp:RequiredFieldValidator>
															<ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
																TargetControlID="CV_Name" PopupPosition="Right" HighlightCssClass="errormassage"
																WarningIconImageUrl="../images/050.png">
															</ajaxToolkit:ValidatorCalloutExtender>
														</td>
														<td class="innerfield_title" width="90">
															<asp:Label ID="Label24" Text="SHORT NAME " runat="server"></asp:Label>
															<span class="req">*</span>
														</td>
														<td width="170">
															<asp:TextBox ID="txtshortname" runat="server" ValidationGroup="Submit" CssClass="large"
																MaxLength="20"> </asp:TextBox>
															<asp:RequiredFieldValidator ID="rfv_txtshortname" runat="server" ValidationGroup="Submit"
																Display="None" ErrorMessage="Short name is required!" ControlToValidate="txtshortname"
																ValidateEmptyText="false" SetFocusOnError="true"> </asp:RequiredFieldValidator>
															<ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender15" runat="server"
																TargetControlID="rfv_txtshortname" PopupPosition="BottomLeft" HighlightCssClass="errormassage"
																WarningIconImageUrl="../images/050.png">
															</ajaxToolkit:ValidatorCalloutExtender>
														</td>
														<td class="innerfield_title" width="90">
															<asp:Label ID="Label37" Text="PRINT NAME" runat="server"></asp:Label>
                                                            <span class="req">*</span>

														</td>
														<td>
															<asp:TextBox ID="txtprintname" runat="server" Width="200" ValidationGroup="submit"> </asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ValidationGroup="Submit"
																Display="None" ErrorMessage="Print name is required!" ControlToValidate="txtprintname"
																ValidateEmptyText="false" SetFocusOnError="true"> </asp:RequiredFieldValidator>
															<ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server"
																TargetControlID="RequiredFieldValidator10" PopupPosition="BottomLeft" HighlightCssClass="errormassage"
																WarningIconImageUrl="../images/050.png">
															</ajaxToolkit:ValidatorCalloutExtender>
														</td>
													</tr>
												</table>
											</td>
										</tr>
										<tr>
											<td class="field_title">Busniess Segment <span class="req">*</span>
											</td>
											<td class="field_input">
												<table width="100%" cellpadding="0" cellspacing="0" border="0">
													<tr>
														<td width="28%">
															<asp:DropDownList ID="ddlBusniessSegment" runat="server" data-placeholder="Choose BusinessSegment"
																CssClass="chosen-select" TabIndex="4" Width="280" AutoPostBack="true"
																AppendDataBoundItems="True" OnSelectedIndexChanged="ddlBusniessSegment_SelectedIndexChanged">
															</asp:DropDownList>
															<asp:RequiredFieldValidator ID="rfv_ddlBusniessSegment" runat="server" ErrorMessage="BusinessSegment is rquired!"
																ForeColor="Red" ControlToValidate="ddlBusniessSegment" ValidateEmptyText="false"
																SetFocusOnError="true" InitialValue="0" ValidationGroup="Submit"></asp:RequiredFieldValidator>
														</td>
														<td width="2%"></td>
														<td width="6%" class="innerfield_title">Group <span class="req">*</span>
														</td>
														<td width="28%">
															<asp:DropDownList ID="ddlGroupType" runat="server" data-placeholder="Choose Group" CssClass="chosen-select"
																TabIndex="4" Width="280" OnSelectedIndexChanged="ddlGroupType_SelectedIndexChanged"
																AutoPostBack="true">
															</asp:DropDownList>
															<asp:RequiredFieldValidator ID="rfv_ddlGroupType" runat="server" ErrorMessage="Group is rquired!"
																ForeColor="Red" ControlToValidate="ddlGroupType" ValidateEmptyText="false" SetFocusOnError="true"
																InitialValue="0" ValidationGroup="Submit"></asp:RequiredFieldValidator>
														</td>
														<td>&nbsp;
														</td>
													</tr>
												</table>
											</td>
										</tr>
										<tr>
											<td class="field_title">
												<asp:Label ID="Label7" Text="Customer Type" runat="server"></asp:Label>
												<span class="req">*</span>
											</td>
											<td align="left" valign="top" class="field_input">
												<table width="100%" cellpadding="0" cellspacing="0" border="0">
													<tr>
														<td width="28%">
															<asp:DropDownList ID="ddlCustomer1" Width="250" runat="server" ValidationGroup="Submit"
																class="chosen-select" data-placeholder="Select Customer Type" AppendDataBoundItems="True"
																OnSelectedIndexChanged="ddlCustomer1_SelectedIndexChanged" AutoPostBack="true">
															</asp:DropDownList>
															<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Submit"
																ForeColor="Red" ErrorMessage="Required!" ControlToValidate="ddlCustomer1" ValidateEmptyText="false"
																SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>
														</td>
														<td class="field_title" width="80px">
															<asp:Label ID="Label29" Text="Purchase Type" runat="server"></asp:Label>
															<span class="req">*</span>
														</td>
														<td>
															<asp:DropDownList ID="lstpurchasetype" runat="server" data-placeholder="Choose Purchse Type"
																CssClass="chosen-select" Width="280" AppendDataBoundItems="True" AutoPostBack="true" ValidationGroup="Submit">
															</asp:DropDownList>
															<asp:RequiredFieldValidator ID="RequiredFieldValidator_lstpurchasetype" runat="server"
																ErrorMessage="Required!" ForeColor="Red" ControlToValidate="lstpurchasetype"
																SetFocusOnError="true" InitialValue="" ValidationGroup="Submit"></asp:RequiredFieldValidator>
														</td>
														<td colspan="2">&nbsp;</td>
													</tr>
												</table>
											</td>
										</tr>
                                        <tr id="trSO" runat="server">
											<td class="field_title">
												<asp:Label ID="Label38" Text="SO Name" runat="server"></asp:Label>
											</td>
											<td align="left" valign="top" class="field_input">
												<table width="100%" cellpadding="0" cellspacing="0" border="0">
													<tr>
														<td width="28%">
															<asp:DropDownList ID="ddlSO" Width="200" runat="server" class="chosen-select"
													                            data-placeholder="Choose SO" AppendDataBoundItems="True">
												            </asp:DropDownList>
														</td>
														<td class="field_title" width="80px">
															<asp:Label ID="Label40" Text="Selling Brand" runat="server"></asp:Label>
														</td>
														<td>
															<asp:DropDownList   ID="ddlBrandType" Width="200" runat="server" class="chosen-select"
													                            data-placeholder="Choose Brand Type" AppendDataBoundItems="True" 
                                                                                OnSelectedIndexChanged="ddlBrandType_SelectedIndexChanged" AutoPostBack="true">
												            </asp:DropDownList>
														</td>
														<td colspan="2">&nbsp;
														</td>
													</tr>
												</table>
											</td>
										</tr>
										<tr id="trBrandMap" runat="server">
                                            <td>&nbsp;</td>
                                            <td >
                                                <div class="gridcontent-inner" id="trinvoice" runat="server" style="width:50%">
                                                <fieldset>
                                                <legend>BRAND LIST</legend>
                                                
                                                    <asp:GridView ID="gvBrandTypeMap" runat="server" Width="100%" CssClass="reportgrid"
                                                        AutoGenerateColumns="false" ShowFooter="false" AlternatingRowStyle-BackColor="AliceBlue"
                                                        EmptyDataText="No Records Available" OnRowDataBound="OnRowDataBound">
                                                        <Columns>
                                                            <asp:TemplateField >
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="chkAllSelect" runat="server" onclick="CheckAllheader(this);" Text=" " />
                                                                </HeaderTemplate>
                                                                <HeaderStyle Width="2px" />
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkSelect" runat="server" Text=" "  class="round" Style="float:left; padding-right:1px;" ToolTip='<%# Bind("BRANDID") %>' onclick="setrowcolor(this);" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        
                                                            <asp:TemplateField HeaderText="BRANDID" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBRANDID" runat="server" Text='<%# Bind("BRANDID") %>'
                                                                        value='<%# Eval("BRANDID") %>' Visible="false"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        
                                                            <asp:TemplateField HeaderText="Sl.No." ItemStyle-Wrap="false" ItemStyle-Width="2px" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <nav style="position: relative;"><span class="badge black"><%# Container.DataItemIndex + 1 %></span></nav>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        
                                                            <asp:TemplateField HeaderText="Brand" ItemStyle-Width="80" ItemStyle-Wrap="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBRANDNAME" runat="server" Text='<%# Bind("BRANDNAME") %>'
                                                                         value='<%# Eval("BRANDNAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="TAG" ItemStyle-Width="60" ItemStyle-Wrap="false"  Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTAG" runat="server" Text='<%# Bind("TAG") %>' 
                                                                        value='<%# Eval("TAG") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        
                                                        
                                                        </Columns>
                                                    </asp:GridView>
                                                
                                                </fieldset>
                                                </div>
                                            </td>
										</tr>
										<tr id="trRetDiv" runat="server">
											<td class="field_title">Retailer Division
											</td>
											<td class="field_input">
												<asp:DropDownList ID="ddlRetDiv" Width="150" runat="server" class="chosen-select"
													data-placeholder="Select Retailer Division" AppendDataBoundItems="True">
												</asp:DropDownList>
											</td>

										</tr>
										<tr>
											<td class="field_title">Contact Person
											</td>
											<td class="field_input">
												<table width="100%" border="0" cellpadding="0" cellspacing="0">
													<tr>
														<td width="24%">
															<asp:TextBox ID="txtContactPerson1" runat="server" ValidationGroup="Submit" CssClass="x-large"></asp:TextBox>
															
															<span class="label_intro">
																<asp:Label ID="Label3" Text=" First Contact Person" runat="server"></asp:Label><span class="req">&nbsp;</span></span>
														</td>
														<td width="15"></td>
														<td width="24%">
															<asp:TextBox ID="txtContactPerson2" runat="server" CssClass="x-large"></asp:TextBox>
															<span class="label_intro">
																<asp:Label ID="Label4" Text=" Second Contact Person" runat="server"></asp:Label></span>
														</td>
														<td>&nbsp;
														</td>
													</tr>
												</table>
											</td>
										</tr>
										<tr>
											<td class="field_title">Contact Info
											</td>
											<td align="left" class="field_input">
												<table width="100%" border="0" cellpadding="0" cellspacing="0">
													<tr>
														<td width="22%">
															<asp:TextBox ID="txtMobileNo" runat="server" MaxLength="10" AutoCompleteType="Disabled"
																ValidationGroup="Submit" onfocus="disableautocompletion(this.id);" onkeypress="return isNumberKey(event);"></asp:TextBox>
															
															<span class=" label_intro">Mobile No 1 <span class="req">&nbsp;</span></span>
														</td>
														<td width="15">&nbsp;
														</td>
														<td width="22%">
															<asp:TextBox ID="txtPhoneNo" runat="server" MaxLength="17" onkeypress="return isNumberKey(event);"></asp:TextBox>
															<asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtPhoneNo"
																ErrorMessage="Minimum 10 digits PhoneNo required." ValidationExpression="^[\s\S]{8,}$"
																Display="None" SetFocusOnError="true"></asp:RegularExpressionValidator>
															<ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server"
																TargetControlID="RegularExpressionValidator3" PopupPosition="Right" HighlightCssClass="errormassage"
																WarningIconImageUrl="../images/050.png">
															</ajaxToolkit:ValidatorCalloutExtender>
															<span class=" label_intro">LandLine No 1</span>
														</td>
														<td width="15">&nbsp;
														</td>
														<td width="22%">
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
														<td width="15">&nbsp;
														</td>
														<td width="22%">
															<asp:TextBox ID="txtPhoneNo1" runat="server" MaxLength="17" onkeypress="return isNumberKey(event);"></asp:TextBox>
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
											<td class="field_title">Customer Email ID
											</td>
											<td class="field_input">
												<table width="100%" border="0" cellpadding="0" cellspacing="0">
													<tr>
														<td width="24%">
															<asp:TextBox ID="txtEmailid1" runat="server" MaxLength="100" 
																CssClass="x-large" Style="text-transform: lowercase;"> </asp:TextBox>
															<%--<asp:RequiredFieldValidator ID="CV_txtEmailid1" runat="server" ValidationGroup="Submit"
																Display="None" ErrorMessage="Email Id  is required!" ControlToValidate="txtEmailid1"
																SetFocusOnError="true"> </asp:RequiredFieldValidator>
															<ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender16" runat="server"
																TargetControlID="CV_txtEmailid1" PopupPosition="Right" HighlightCssClass="errormassage"
																WarningIconImageUrl="../images/050.png">
															</ajaxToolkit:ValidatorCalloutExtender>
															<asp:RegularExpressionValidator ID="CV_email_valid1" runat="server"
																ErrorMessage="Enter Valid Email ID!" ControlToValidate="txtEmailid1" SetFocusOnError="true"
																ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"> </asp:RegularExpressionValidator>
															<ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
																TargetControlID="CV_email_valid1" PopupPosition="Right" HighlightCssClass="errormassage"
																WarningIconImageUrl="../images/050.png">
															</ajaxToolkit:ValidatorCalloutExtender>--%>
															<span class="label_intro">
																<asp:Label ID="Label1" Text=" First Email id" runat="server"></asp:Label><span class="req">&nbsp;</span></span>
														</td>
														<td width="15">&nbsp
														</td>
														<td width="24%">
															<asp:TextBox ID="txtEmailid2" runat="server" MaxLength="100" CssClass="x-large" Style="text-transform: lowercase;"> </asp:TextBox>
															<span class="label_intro">
																<asp:Label ID="Label2" Text=" Second Email id" runat="server"></asp:Label></span>
														</td>
														<td>&nbsp;
														</td>
													</tr>
												</table>
											</td>
										</tr>
										<tr>
											<td class="field_title">Location <span class="req">*</span>
											</td>
											<td class="field_input">
												<div id="divstate" runat="server" style="display: block">
													<table width="100%" cellpadding="0" cellspacing="0" border="0">
														<tr>
															<td width="18%">
																<asp:DropDownList ID="ddlState" runat="server" ValidationGroup="Submit" AppendDataBoundItems="True"
																	 class="chosen-select" data-placeholder="Select State" Width="160"
																	>
																</asp:DropDownList>
																<span class="label_intro">
																	<asp:Label ID="Label13" Text="&nbsp;State" runat="server"></asp:Label><span class="req">&nbsp;*</span>
																</span>
															</td>
															<td width="18%">
																<%--<asp:DropDownList ID="ddlDistrict" runat="server" ValidationGroup="Submit" AppendDataBoundItems="True"
																	AutoPostBack="true" class="chosen-select" data-placeholder="Select District"
																	Width="160" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
																</asp:DropDownList>--%>
                                                                   <asp:TextBox ID="txtDistrict" runat="server" CssClass="x-large"> </asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="CV_District" runat="server" Display="None" ErrorMessage="District is required!"
                                                                ControlToValidate="txtDistrict" ValidateEmptyText="false" SetFocusOnError="true"
                                                                ValidationGroup="Submit"> </asp:RequiredFieldValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server"
                                                                TargetControlID="CV_District" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                          
																<span class="label_intro">
																	<asp:Label ID="Label15" Text="&nbsp;District" runat="server"></asp:Label><span class="req">&nbsp;*</span>
																</span>
															</td>
															<td width="18%">
																<%--<asp:DropDownList ID="ddlCity" runat="server" ValidationGroup="Submit" AppendDataBoundItems="true"
																	Width="160" class="chosen-select" data-placeholder="Select City">
																</asp:DropDownList>--%>
                                                                    <asp:TextBox ID="txtCity" runat="server" CssClass="x-large"> </asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="CV_City" runat="server" Display="None" ErrorMessage="District is required!"
                                                                ControlToValidate="txtCity" ValidateEmptyText="false" SetFocusOnError="true"
                                                                ValidationGroup="Submit"> </asp:RequiredFieldValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server"
                                                                TargetControlID="CV_City" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
																<span class="label_intro">
																	<asp:Label ID="Label16" Text="&nbsp;City" runat="server"></asp:Label><span class="req">&nbsp;*</span>
																</span>
															</td>
															<td valign="top" id="tdaddCR" runat="server">
																<asp:Button ID="btnAddCity" runat="server" CausesValidation="false" Enabled="false" Visible="false"
																	CssClass="h_icon add_co" ToolTip="Add City" OnClick="btnAddCity_Click" />
																<asp:Button ID="btnRefresh" runat="server" CausesValidation="false" CssClass="h_icon arrow_refresh_co" Visible="false"
																	Enabled="false" OnClick="btnRefresh_Click" ToolTip="Refresh" />
															</td>
														</tr>
													</table>
												</div>
												<div id="divcountry" runat="server" style="display: block">
													<table width="100%" cellpadding="0" cellspacing="0" border="0">
														<tr>
															<td width="18%">
																<asp:DropDownList ID="ddlcountry" runat="server" ValidationGroup="Submit" AppendDataBoundItems="True"
																	class="chosen-select" data-placeholder="Select Country" Width="160">
																</asp:DropDownList>
																<span class="label_intro">
																	<asp:Label ID="Label20" Text="&nbsp;Country" runat="server"></asp:Label><span class="req">&nbsp;*</span>
																</span>
															</td>
														</tr>
													</table>
												</div>
											</td>
										</tr>
										<tr>
											<td class="field_title">Address 1 <span class="req">*</span>
											</td>
											<td align="left" class="field_input">
												<table width="100%" cellpadding="0" cellspacing="0" border="0">
													<tr>
														<td colspan="2" height="2"></td>
													</tr>
													<tr>
														<td width="40%">
															<asp:TextBox ID="txtAddress" runat="server" CssClass=" large" ValidationGroup="Submit"
																TextMode="MultiLine  "></asp:TextBox>
															<asp:RequiredFieldValidator ID="CV_txtAddress" runat="server" ValidationGroup="Submit"
																ControlToValidate="txtAddress" Display="None" ErrorMessage="Address is required!"
																SetFocusOnError="true" ValidateEmptyText="false"> </asp:RequiredFieldValidator>
															<ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server"
																HighlightCssClass="errormassage" PopupPosition="BottomLeft" TargetControlID="CV_txtAddress"
																WarningIconImageUrl="../images/050.png">
															</ajaxToolkit:ValidatorCalloutExtender>
															<span class="label_intro">Address 1 *</span>
														</td>
														<td valign="top" style="padding: 0 0 0 15px;">
															<asp:TextBox ID="txtPIN" runat="server" Width="100px" MaxLength="6" AutoCompleteType="Disabled" ValidationGroup="submit"
																onfocus="disableautocompletion(this.id);" onkeypress="return isNumberKey(event);"> </asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="Submit"
																ControlToValidate="txtPIN" Display="None" ErrorMessage="Pin is required!"
																SetFocusOnError="true" ValidateEmptyText="false"> </asp:RequiredFieldValidator>
															
															<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPIN"
																ErrorMessage="Minimum 6 digits PIN/ZIP required." SetFocusOnError="true"
																ValidationExpression="[0-9]{6}"></asp:RegularExpressionValidator>
															<ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" runat="server"
																HighlightCssClass="errormassage" PopupPosition="Right" TargetControlID="RequiredFieldValidator6"
																WarningIconImageUrl="../images/050.png">
															</ajaxToolkit:ValidatorCalloutExtender>
															<span class="label_intro">Pin/ Zip Code *</span>
														</td>
													</tr>
												</table>
											</td>
										</tr>
										<tr>
											<td class="field_title">
												<asp:Label ID="Label5" Text=" Address 2" runat="server"></asp:Label>
											</td>
											<td class="field_input">
												<table width="100%" cellpadding="0" cellspacing="0" border="0">
													<tr>
														<td width="40%">
															<asp:TextBox ID="txtDeliveryaddress" runat="server" TextMode="MultiLine" CssClass=" large"></asp:TextBox>
															<span class=" label_intro">Address 2</span>
														</td>
														<td valign="top" style="padding: 0 0 0 15px;">
															<asp:TextBox ID="txtDeliveryPIN" runat="server" MaxLength="6" Width="100px"> </asp:TextBox>
															<span class=" label_intro">Pin/ Zip Code</span>
														</td>
														<td>&nbsp;
														</td>
													</tr>
												</table>
											</td>
										</tr>
										<tr>
											<td class="field_title"></td>
											<td class="field_title">
												<asp:Label ID="Label32" runat="server" Text="DOB"></asp:Label>&nbsp;
                                                <asp:TextBox ID="txtDOB" runat="server" MaxLength="10" Width="90" Enabled="false"
													placeholder="dd/MM/yyyy" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="Search"
													Font-Bold="true"></asp:TextBox>
												<asp:ImageButton ID="imgPopup" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
													runat="server" Height="24" />
												<ajaxToolkit:CalendarExtender ID="CalendarFromDate" PopupButtonID="imgPopup" runat="server"
													TargetControlID="txtDOB" Format="dd/MM/yyyy" CssClass="cal_Theme1">
												</ajaxToolkit:CalendarExtender>
												&nbsp;
                                                <asp:Label ID="Label33" runat="server" Text="ANV.DATE"></asp:Label>&nbsp;
                                                <asp:TextBox ID="txtAnvDate" runat="server" MaxLength="10" Width="90" Enabled="false"
													placeholder="dd/mm/yyyy" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="Search"
													Font-Bold="true"></asp:TextBox>
												<asp:ImageButton ID="imgPopuptodate" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
													runat="server" Height="24" />
												<ajaxToolkit:CalendarExtender ID="CalendarExtenderToDate" PopupButtonID="imgPopuptodate"
													runat="server" TargetControlID="txtAnvDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
												</ajaxToolkit:CalendarExtender>
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
															<asp:DropDownList ID="ddlAccGroup" Width="250" runat="server" data-placeholder="Select Group Name"
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
											<td class="field_title">
												<asp:Label ID="Label14" runat="server" Text="Reporitng To (Role)"></asp:Label>
												<span class="req">*</span>
											</td>
											<td class="field_input">
												<asp:DropDownList ID="ddlReportToRole" runat="server" class="chosen-select" ValidationGroup="Submit"
													data-placeholder="Select" Width="250" OnSelectedIndexChanged="ddlReportToRole_SelectedIndexChanged"
													AutoPostBack="true">
													<asp:ListItem Value="0" Text="Select Reporting Role"></asp:ListItem>
												</asp:DropDownList>
												<asp:RequiredFieldValidator ID="RequiredFieldValidatorReportToRole" ValidationGroup="Submit"
													runat="server" ControlToValidate="ddlReportToRole" InitialValue="0" SetFocusOnError="true"
													ErrorMessage="Required!" Display="None"></asp:RequiredFieldValidator>
												<ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtenderReportToRole" runat="server"
													TargetControlID="RequiredFieldValidatorReportToRole" PopupPosition="Right" HighlightCssClass="errormassage"
													WarningIconImageUrl="../images/050.png">
												</ajaxToolkit:ValidatorCalloutExtender>
											</td>
										</tr>
										<tr>
											<td align="left" class="field_title">Reporting To (User) <span class="req">*</span>
											</td>
											<td align="left" valign="top" class="field_input">
												<asp:DropDownList ID="ddlUnder" Width="250" runat="server" ValidationGroup="Submit"
													class="chosen-select" data-placeholder="Select Reporting To" AppendDataBoundItems="True">
												</asp:DropDownList>
												<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Submit"
													ForeColor="Red" ErrorMessage="Required!" ControlToValidate="ddlUnder" ValidateEmptyText="false"
													SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>
											</td>
										</tr>
										<tr>
											<td align="left" class="field_title">Credit's Limit (if any)
											</td>
											<td class="field_input">
												<table width="100%" cellpadding="0" cellspacing="0" border="0">
													<tr>
														<td width="12%">
															<asp:TextBox ID="txtpercentage" runat="server" ValidationGroup="Submit" MaxLength="7"
																Width="100px" onkeypress="return validateFloatKeyPress(this,event);"></asp:TextBox>
															<asp:RequiredFieldValidator ID="rfv_txtpercentage" runat="server" ValidationGroup="Submit"
																Display="None" ErrorMessage="Credit Days required" ControlToValidate="txtpercentage"
																SetFocusOnError="true">
															</asp:RequiredFieldValidator>
															<ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server"
																TargetControlID="rfv_txtpercentage" PopupPosition="Right" HighlightCssClass="errormassage"
																WarningIconImageUrl="../images/050.png">
															</ajaxToolkit:ValidatorCalloutExtender>
															<span class="label_intro">
																<asp:Label ID="Label11" Text="DAYS *" runat="server"></asp:Label></span>
														</td>
														<td width="18%">
															<asp:TextBox ID="txtamount" runat="server" ValidationGroup="Submit" MaxLength="10"
																Width="100px" onkeypress="return validateFloatKeyPress(this,event);"></asp:TextBox>
															<asp:RequiredFieldValidator ID="rfv_txtamount" runat="server" ValidationGroup="Submit"
																Display="None" ErrorMessage="Amount is required" ControlToValidate="txtamount"
																SetFocusOnError="true">
															</asp:RequiredFieldValidator>
															<ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
																TargetControlID="rfv_txtamount" PopupPosition="Right" HighlightCssClass="errormassage"
																WarningIconImageUrl="../images/050.png">
															</ajaxToolkit:ValidatorCalloutExtender>
															<span class="label_intro">
																<asp:Label ID="Label12" Text="AMOUNT *" runat="server"></asp:Label></span>
														</td>
														<td width="8%" class="innerfield_title">Currency <span class="req">*</span>
														</td>
														<td width="30">
															<asp:DropDownList ID="ddlcurrency" Width="200" runat="server" class="chosen-select"
																ValidationGroup="Submit" data-placeholder="Select Currency" AppendDataBoundItems="True">
															</asp:DropDownList>
															<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="Submit"
																ForeColor="Red" ErrorMessage="Required!" ControlToValidate="ddlcurrency" ValidateEmptyText="false"
																SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>
														</td>
													</tr>
												</table>
											</td>
										</tr>

                                        <tr>
											<td class="field_title">TCS
											</td>
											<td class="field_input">
												<table cellpadding="0" cellspacing="0" border="0">
													<tr>
														<td width="10%">
															<asp:TextBox ID="txttcspercentage" runat="server" ValidationGroup="Submit" MaxLength="7"
																Width="100px" onkeypress="return validateFloatKeyPress(this,event);"></asp:TextBox>
															<asp:RequiredFieldValidator ID="rfv_txttcspercentage" runat="server" ValidationGroup="Submit"
																Display="None" ErrorMessage="TCS percentage is required" ControlToValidate="txttcspercentage"
																SetFocusOnError="true">
															</asp:RequiredFieldValidator>
															<ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server"
																TargetControlID="rfv_txttcspercentage" PopupPosition="Right" HighlightCssClass="errormassage"
																WarningIconImageUrl="../images/050.png">
															</ajaxToolkit:ValidatorCalloutExtender>
															<span class="label_intro">
																<asp:Label ID="Label42" Text="PERCENTAGE(%)" runat="server"></asp:Label></span>
														</td>														
													</tr>
												</table>
											</td>
										</tr>

										<tr>
											<td class="field_title">Customer Other's Info 
											</td>
											<td class="field_input">
												<table border="0" cellpadding="0" cellspacing="0">
													<tr>
														<td>
															<asp:TextBox ID="txtPanNO" runat="server" CssClass="x-large" MaxLength="10" Style="text-transform: uppercase;"
																onkeyup="panno()" onchange="CHKPAN();" onkeypress="return isLetterNumberKey(event);"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ValidationGroup="Submit"
																ControlToValidate="txtPanNO" Display="None" ErrorMessage="PAN NO is required!"
																SetFocusOnError="true" ValidateEmptyText="false"> </asp:RequiredFieldValidator>
															<ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
																HighlightCssClass="errormassage" PopupPosition="BottomLeft" TargetControlID="RequiredFieldValidator9"
																WarningIconImageUrl="../images/050.png">
															</ajaxToolkit:ValidatorCalloutExtender>
                                                              
															<span class="label_intro">
																<asp:Label ID="Label17" Text="PAN NO * (ex:AAACJ4124B)" runat="server"></asp:Label>
														</td>
														<td width="15">&nbsp
														</td>
														<td width="22%">
															<asp:TextBox ID="txttinno" runat="server" ValidationGroup="Submit" CssClass="x-large"
																MaxLength="15" Style="text-transform: uppercase;" Visible="false"> </asp:TextBox>
															<span class="label_intro">
																<asp:Label ID="lbltinno" Text="TIN NO" runat="server" Visible="false"></asp:Label></span>
														</td>
														<td width="15">&nbsp
														</td>
														<td width="22%">
															<asp:TextBox ID="txtvatno" runat="server" 
                                                                CssClass="x-large" MaxLength="15" Visible="false" Style="text-transform: uppercase;"> </asp:TextBox>
															<span class="label_intro">
																<asp:Label ID="lblvatno" Text="VAT NO" Visible="false" runat="server"></asp:Label></span>
														</td>
														<td width="15">&nbsp
														</td>
														<td width="22%">
															<asp:TextBox ID="txtcstno" runat="server" CssClass="x-large" MaxLength="15" Visible="false" Style="text-transform: uppercase;"> </asp:TextBox>
															<span class="label_intro">
																<asp:Label ID="lblcstno" Text="CST NO" Visible="false" runat="server"></asp:Label></span>

														</td>
														<td width="15">&nbsp
														</td>

													</tr>
												</table>
											</td>
										</tr>
										<tr>
											<td align="left" class="field_title">
												<asp:Label ID="Label18" Text="IS GST APPLICABLE" runat="server"></asp:Label>
											</td>
											<td align="left" class="field_input" width="10">
												<asp:CheckBox ID="chkgst" runat="server" Text=" " AutoPostBack="true" OnCheckedChanged="chkgst_CheckedChanged" />
											</td>
										</tr>
										<tr>
											<td class="field_title">GSTIN
											</td>
											<td class="field_input">
												<table border="0" cellpadding="0" cellspacing="0">
													<tr>
														<td>
															<asp:TextBox ID="txtstatecode" runat="server" MaxLength="15" Enabled="false" Width="20px"></asp:TextBox>
														</td>
														<td>
															<asp:TextBox ID="txtgstpanno" runat="server" MaxLength="15" Enabled="false" Width="100px"
																Style='text-transform: uppercase'></asp:TextBox>
														</td>
														<td>
															<asp:TextBox ID="txtgstno"  
                                                             
                                                                runat="server" MaxLength="03" Width="30" onchange="CHKGSTM();"
                                                                Style='text-transform: uppercase'
                                                                 onkeyup="return isLetterNumberKey3D(event);"
                                                                 onkeypress="return isLetterNumberKey(event);" 
                                                                ></asp:TextBox>
														</td>
													</tr>
												</table>
											</td>
										</tr>
										<tr id="trshop" runat="server">
											<td class="field_title">
												<asp:Label ID="lblshop" Text="Shop Type (if any)" runat="server"></asp:Label>
											</td>
											<td align="left" valign="top" class="field_input">
												<table border="0" cellpadding="0" cellspacing="0">
													<tr>
														<td width="80">
															<asp:DropDownList ID="ddlShop" Width="250" runat="server" ValidationGroup="Submit"
																class="chosen-select" data-placeholder="Choose Shop Type" AppendDataBoundItems="True">
															</asp:DropDownList>
														</td>
														<td>&nbsp;
														</td>
														<td width="120" class="field_title">&nbsp;
														</td>
														<td>&nbsp;
														</td>
														<td>&nbsp;
														</td>
													</tr>
												</table>
											</td>
										</tr>






										<tr id="trcategory" runat="server">
											<td class="field_title">
												<asp:Label ID="Label36" Text="D.Category" runat="server"></asp:Label>
											</td>
											<td align="left" valign="top" class="field_input">
												<table border="0" cellpadding="0" cellspacing="0">
													<tr>
														<td align="left" valign="top" class="field_input">
															<asp:DropDownList ID="ddlretailcat" Width="250" runat="server" class="chosen-select"
																data-placeholder="Choose  Category" AppendDataBoundItems="True">
															</asp:DropDownList>
															<td>&nbsp;
															</td>
															<td width="120" class="field_title">&nbsp;
															</td>
															<td>&nbsp;
															</td>
															<td>&nbsp;
															</td>
														</td>

													</tr>
												</table>
											</td>
										</tr>


										<tr>

											<td align="left" class="field_title">
												<asp:Label ID="lblactive" Text="Active" runat="server"></asp:Label>
											</td>
											<td align="left" class="field_input">
												<asp:CheckBox ID="chkActive" runat="server" Text=" " onclick="chkactive(this)" />
											</td>
										</tr>
										<tr id="trinactivedate" class="field_title" runat="server">
											<td></td>
											<td>
												<asp:Label ID="Label39" runat="server" Text="INACTIVE DATE" ForeColor="Red"></asp:Label>&nbsp;
                                                <asp:TextBox ID="txtInactiveDate" runat="server" MaxLength="10" Width="90" Enabled="false"
													placeholder="dd/mm/yyyy" onkeypress="return isNumberKeyWithslash(event);"
													Font-Bold="true"></asp:TextBox>
												<asp:ImageButton ID="imgPopupinactivedate" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
													runat="server" Height="24" />
												<ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPopupinactivedate"
													runat="server" TargetControlID="txtInactiveDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
												</ajaxToolkit:CalendarExtender>
											</td>
										</tr>
										<tr>
											<td align="left" class="field_title">
												<asp:Label ID="Label27" Text="Additinal Super Stockist Margin" runat="server" Visible="false"></asp:Label>
											</td>
											<td align="left" class="field_input">
												<asp:CheckBox ID="chkaddssmagine" runat="server" Text=" " onclick="checkadds(this)" Visible="false" />
											</td>
										</tr>
										<tr id="trMarginPer" runat="server">
											<td align="left" class="field_title">
												<asp:Label ID="Label28" Text="Margin Percentage" runat="server"></asp:Label>
											</td>
											<td class="field_input">
												<asp:TextBox ID="txtaddmarginpercentage" runat="server" Width="80px"> </asp:TextBox>
												
                                                &nbsp;&nbsp;&nbsp;
                                                <asp:Label ID="Label30" Text="IS TRANSFER TO HO" runat="server" Style="display: none"></asp:Label>
												&nbsp;
                                                <asp:CheckBox ID="chktransfertoho" runat="server" Text=" " Style="display: none" />
											</td>
										</tr>
										<tr>
											<td align="left">&nbsp;
											</td>
											<td align="left" style="padding: 8px 0;">
												<div class="btn_24_blue" id="divbtnCustomerSubmit" runat="server">
													<span class="icon disk_co"></span>
													<asp:Button ID="btnCustomerSubmit" runat="server" Text="Save" ValidationGroup="Submit"
														CssClass="btn_link" OnClick="btnCustomerSubmit_Click" OnClientClick="Confirm(this)" />
												</div>
												&nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_blue">
													<span class="icon cross_octagon_co"></span>
													<asp:Button ID="btnTPUCancel" runat="server" Text="Cancel" CssClass="btn_link" CausesValidation="false"
														OnClick="btnTPUCancel_Click" />
												</div>
												<asp:HiddenField ID="Hdn_Fld" runat="server" />
												<asp:HiddenField ID="hdnCustCategory" runat="server" />
												<asp:HiddenField ID="Hdn_Vatexemp" runat="server" />
												<asp:HiddenField ID="Hdn_controltagE" runat="server" />
												<asp:HiddenField ID="Hdn_controltagD" runat="server" />
											</td>
										</tr>
									</table>
								</asp:Panel>

								<asp:Panel ID="pnlDepotMapping" runat="server">
									<table width="100%" border="0" cellspacing="0" cellpadding="0" class="form_container_td">
										<tr>
											<td class="field_title" width="120">
												<asp:Label ID="lblCustomername" Text="CUSTOMER NAME" runat="server"></asp:Label><span
													class="req">*</span>
											</td>
											<td class="field_input">
												<asp:TextBox ID="txtCustomername" runat="server" CssClass="large" Enabled="false"></asp:TextBox>
											</td>
										</tr>
										<tr>
											<td colspan="2" class="field_input" style="padding-left: 10px;">
												<fieldset style="width: 50%">
													<legend>DEPOT MAPPING</legend>
													<div style="width: 100%;">
														<div style="overflow: hidden;" id="DivHeaderRow">
														</div>
														<div id="DivMainContent" style="overflow: scroll; height: 300px;" onscroll="OnScrollDiv(this);">
															<asp:GridView ID="gvCustomerDepotMapping" runat="server" Width="100%" CssClass="zebra" AutoGenerateColumns="false"
																EmptyDataText="No Records Available">
																<Columns>
																	<asp:TemplateField>
																		<HeaderTemplate>
																			<asp:CheckBox ID="chkAllSelectvou" runat="server" onclick="CheckAllheadervou(this);" Text=" " />
																		</HeaderTemplate>
																		<HeaderStyle Width="5px" />
																		<ItemTemplate>
																			<asp:CheckBox ID="chkSelectvou" runat="server" Text=" " class="round" Style="padding-left: 15px;"
																				ToolTip='<%# Bind("BRID") %>' onclick="setrowcolorvou(this);" />
																		</ItemTemplate>
																	</asp:TemplateField>
																	<asp:TemplateField HeaderText="BRID" Visible="false">
																		<ItemTemplate>
																			<asp:Label ID="lblBRID" runat="server" Text='<%# Bind("BRID") %>'
																				value='<%# Eval("BRID") %>' Visible="false"></asp:Label>
																		</ItemTemplate>
																	</asp:TemplateField>
																	<asp:TemplateField HeaderText="BRNAME">
																		<ItemTemplate>
																			<asp:Label ID="lblBRNAME" runat="server" Text='<%# Bind("BRNAME") %>'
																				value='<%# Eval("BRNAME") %>'></asp:Label>
																		</ItemTemplate>
																	</asp:TemplateField>
																</Columns>
															</asp:GridView>
														</div>
														<div id="DivFooterRow" style="overflow: hidden;">
														</div>
													</div>
												</fieldset>
											</td>
										</tr>
										<tr>
											<td colspan="2" style="padding: 8px 10px;">
												<div class="btn_24_blue">
													<span class="icon disk_co"></span>
													<asp:Button ID="btnCustDepotMapSubmit" runat="server" Text="Save" CssClass="btn_link"
														CausesValidation="false" OnClick="btnCustDepotMapSubmit_Click" />
												</div>
												&nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_blue">
													<span class="icon cross_octagon_co"></span>
													<asp:Button ID="btnCustDepotMapCancel" runat="server" Text="Cancel" CssClass="btn_link"
														CausesValidation="false" OnClick="btnCustDepotMapCancel_Click" />
												</div>
												&nbsp;&nbsp;&nbsp;&nbsp;
                                                 <asp:Label ID="lbltotalcount" runat="server" Text="" Font-Bold="true" ForeColor="Gray"
													 Font-Size="Small"></asp:Label>
											</td>
										</tr>
									</table>
								</asp:Panel>

								<asp:Panel ID="pnlProductMapping" runat="server">
									<table width="100%" border="0" cellspacing="0" cellpadding="0" class="form_container_td">
										<tr>
											<td width="120" class="field_title">
												<asp:Label ID="Label6" Text="CUSTOMER NAME" runat="server"></asp:Label>
											</td>
											<td class="field_input">
												<asp:TextBox ID="txtCustomerproductname" runat="server" CssClass="large" Enabled="false"></asp:TextBox>
											</td>
										</tr>
										<tr>
											<td colspan="2" class="field_input" style="padding-left: 10px;">
												<fieldset>
													<legend>PRODUCT MAPPING</legend>
													<table border="0" cellspacing="0" cellpadding="0" class="form_container_td">
														<tr>
															<td width="60" class="field_title">
																<asp:Label ID="lblDiv" Text="Brand" runat="server"></asp:Label>&nbsp;<span class="req">*</span>
															</td>
															<td width="238" class="field_input">
																<asp:DropDownList ID="ddlBrand" runat="server" AppendDataBoundItems="true" Width="230px"
																	class="chosen-select" data-placeholder="Choose a Brand" OnSelectedIndexChanged="ddlBrand_SelectedIndexChanged"
																	AutoPostBack="true">
																</asp:DropDownList>
																<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Required!"
																	Font-Bold="true" ForeColor="Red" ControlToValidate="ddlBrand" ValidateEmptyText="false"
																	SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>
															</td>
															<td width="80" class="field_title">
																<asp:Label ID="lblCat" Text="Category" runat="server"></asp:Label>&nbsp;<span class="req">*</span>
															</td>
															<td class="field_input">
																<asp:DropDownList ID="ddlCategory" runat="server" AppendDataBoundItems="true" Width="230px"
																	class="chosen-select" data-placeholder="Choose a Category" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"
																	AutoPostBack="true">
																</asp:DropDownList>
																<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Required!"
																	EnableClientScript="true" ForeColor="Red" ControlToValidate="ddlCategory" ValidateEmptyText="false"
																	SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>
															</td>
														</tr>
														<tr>
															<td class="field_input" colspan="4">
																<div class="gridcontent-short">
																	<ul>
																		<li class="left">
																			<cc1:Grid ID="gvproductselect" runat="server" FolderStyle="../GridStyles/premiere_blue"
																				AutoGenerateColumns="false" AllowSorting="false" AllowPageSizeSelection="true"
																				PageSize="300" AllowPaging="false" AllowFiltering="false" Serialize="true" AllowAddingRecords="false">
																				<FilteringSettings InitialState="Visible" />
																				<Columns>
																					<cc1:Column ID="Column3" DataField="NAME" HeaderText="PRODUCT NAME " runat="server"
																						Width="350" Wrap="true">
																						<FilterOptions>
																							<cc1:FilterOption Type="NoFilter" />
																							<cc1:FilterOption Type="Contains" />
																							<cc1:FilterOption Type="StartsWith" />
																						</FilterOptions>
																					</cc1:Column>
																					<cc1:Column ID="Column4" DataField="ID" ReadOnly="true" HeaderText="ID" runat="server"
																						Width="50" Wrap="true">
																						<TemplateSettings TemplateId="CheckProductTemplate" HeaderTemplateId="HeaderTemplate" />
																					</cc1:Column>
																					<cc1:CheckBoxSelectColumn ID="CheckBoxSelectColumn1" HeaderText="" DataField="" ShowHeaderCheckBox="true"
																						ControlType="Obout" runat="server">
																					</cc1:CheckBoxSelectColumn>
																				</Columns>
																				<FilteringSettings MatchingType="AnyFilter" />
																				<Templates>
																				</Templates>
																				<ScrollingSettings ScrollWidth="100%" ScrollHeight="180" />
																			</cc1:Grid>
																		</li>
																		<li class="middle">
																			<asp:Button ID="btnproductadd" runat="server" Text="" CausesValidation="false" CssClass="addbtn_blue"
																				OnClick="btnproductadd_Click" />
																		</li>
																		<li class="right">
																			<cc1:Grid ID="gvProductadd" runat="server" Serialize="true" AllowAddingRecords="false"
																				AutoGenerateColumns="false" AllowSorting="true" AllowPageSizeSelection="true"
																				FolderStyle="../GridStyles/premiere_blue" AllowPaging="false" PageSize="100">
																				<Columns>
																					<cc1:Column ID="Column180" DataField="SlNo" HeaderText="SL" runat="server" Width="60">
																						<TemplateSettings TemplateId="slnoTemplateFinal" />
																					</cc1:Column>
																					<cc1:Column ID="Column100" DataField="GUID" HeaderText="GUID" runat="server" Visible="false" />
																					<cc1:Column ID="Column200" DataField="CUSTOMERID" HeaderText="CUSTOMERID" Visible="false"
																						runat="server">
																					</cc1:Column>
																					<cc1:Column ID="Column300" DataField="CUSTOMERNAME" HeaderText="CUSTOMERNAME" Width="50"
																						Visible="false" runat="server">
																					</cc1:Column>
																					<cc1:Column ID="Column400" DataField="BRANDID" HeaderText="BRAND" Visible="false"
																						runat="server">
																					</cc1:Column>
																					<cc1:Column ID="Column5" DataField="BRANDNAME" HeaderText="BRAND" Width="150" Visible="false"
																						runat="server">
																					</cc1:Column>
																					<cc1:Column ID="Column6" DataField="CATEGORYID" HeaderText="CATEGORY" Visible="false"
																						runat="server">
																					</cc1:Column>
																					<cc1:Column ID="Column7" DataField="CATEGORYNAME" HeaderText="CATEGORY" Width="150"
																						Visible="false" runat="server">
																					</cc1:Column>
																					<cc1:Column ID="Column8" DataField="PRODUCTID" HeaderText="PRODUCTID" Visible="false"
																						runat="server">
																					</cc1:Column>
																					<cc1:Column ID="Column9" DataField="PRODUCTNAME" HeaderText="PRODUCTNAME" Width="300"
																						runat="server">
																					</cc1:Column>
																					<cc1:Column HeaderText="Delete" AllowEdit="false" AllowDelete="true" Width="80">
																						<TemplateSettings TemplateId="deleteBtnTemplategroup" />
																					</cc1:Column>
																				</Columns>
																				<Templates>
																					<cc1:GridTemplate runat="server" ID="deleteBtnTemplategroup">
																						<Template>
																							<a href="javascript: //" class="action-icons c-delete" id="btnGridDelete_<%# Container.PageRecordIndex %>"
																								onclick="CallProductDeleteMethod(this)"></a>
																						</Template>
																					</cc1:GridTemplate>
																					<cc1:GridTemplate runat="server" ID="slnoTemplateFinal">
																						<Template>
																							<asp:Label ID="lblslnoTaxFinal" runat="server" Text='<%#Container.PageRecordIndex+1 %>'></asp:Label>
																						</Template>
																					</cc1:GridTemplate>
																				</Templates>
																				<ScrollingSettings ScrollWidth="100%" ScrollHeight="180" />
																			</cc1:Grid>
																			<asp:Button ID="btnproductdelete" runat="server" CausesValidation="false" Text="grddelete"
																				Style="display: none" OnClick="btnproductdelete_Click" OnClientClick="return confirm('Are you sure you want to delete this record?');" />
																			<asp:HiddenField ID="hdnproductdelete" runat="server" />
																			<asp:HiddenField ID="hdn_city" runat="server" />
																			<asp:HiddenField ID="hdncustomername" runat="server" />
																			<asp:HiddenField ID="hdn_latitude" runat="server" />
																			<asp:HiddenField ID="hdn_longitude" runat="server" />
																			<asp:HiddenField ID="Hdn_Fld_Delete" runat="server" />
																			<asp:HiddenField ID="Hdncustid" runat="server" />
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
													<asp:Button ID="btnProductSubmit" runat="server" Text="Save" CssClass="btn_link"
														CausesValidation="false" OnClick="btnProductSubmit_Click" />
												</div>
												&nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_blue">
													<span class="icon cross_octagon_co"></span>
													<asp:Button ID="btnProductCancel" runat="server" Text="Cancel" CssClass="btn_link"
														CausesValidation="false" OnClick="btnProductCancel_Click" />
												</div>
											</td>
										</tr>
									</table>
								</asp:Panel>
								<asp:Panel ID="PnlTransporter" runat="server">
									<table width="100%" border="0" cellspacing="0" cellpadding="0" class="form_container_td">
										<tr>
											<td width="120" class="field_title">
												<asp:Label ID="Label25" Text="Distributor Name" runat="server"></asp:Label>
											</td>
											<td class="field_input">
												<asp:TextBox ID="txtDistrbutor" runat="server" CssClass="large" Enabled="false"></asp:TextBox>
											</td>
										</tr>
										<tr>
											<td colspan="2" class="field_input" style="padding-left: 10px;">
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
																					<cc1:Column ID="Column19" DataField="NAME" HeaderText="TRANSPORTER" runat="server"
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
																					<cc1:Column ID="Column39" DataField="CUSTOMERID" HeaderText="CUSTOMERID" Visible="false"
																						runat="server">
																					</cc1:Column>
																					<cc1:Column ID="Column40" DataField="CUSTOMERNAME" HeaderText="CUSTOMERNAME" Width="50"
																						Visible="false" runat="server">
																					</cc1:Column>
																					<cc1:Column ID="Column41" DataField="TRANSPORTERID" HeaderText="TRANSPORTERID" Visible="false"
																						runat="server">
																					</cc1:Column>
																					<cc1:Column ID="Column42" DataField="TRANSPORTERNAME" HeaderText="NAME" Width="220"
																						runat="server">
																					</cc1:Column>
																					<cc1:Column ID="Column44" DataField="CUSTYPE_ID" HeaderText="NAME" Width="150" runat="server"
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
																			<asp:HiddenField ID="HiddenField5" runat="server" />
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
								<asp:Panel ID="pnltsimapping" runat="server">
									<table width="100%" border="0" cellspacing="0" cellpadding="0" class="form_container_td">
										<tr>
											<td class="field_title" width="30">
												<asp:Label ID="lbltsicustomername" Text="CUSTOMER" runat="server"></asp:Label>
											</td>
											<td class="field_input" width="290">
												<asp:TextBox ID="txttsicustomername" runat="server" Enabled="false" Width="290"></asp:TextBox>
											</td>
										</tr>
										<tr>
											<td class="field_title">
												<asp:Label ID="lbltsi" Text="SALES FORCE" runat="server"></asp:Label>
											</td>
											<td class="field_input">
												<asp:ListBox ID="lbtsi" runat="server" data-placeholder="Choose SalesForce" CssClass="chosen-select"
													SelectionMode="Multiple" TabIndex="4" Width="293" AppendDataBoundItems="True"></asp:ListBox>
											</td>
										</tr>
										<tr>
											<td align="left"></td>
											<td align="left" style="padding: 8px 0;">
												<div class="btn_24_blue">
													<span class="icon disk_co"></span>
													<asp:Button ID="btntsisubmit" runat="server" Text="Save" CssClass="btn_link" CausesValidation="false"
														OnClick="btntsisubmit_Click" />
												</div>
												&nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_blue">
													<span class="icon cross_octagon_co"></span>
													<asp:Button ID="btntsicancel" runat="server" Text="Cancel" CssClass="btn_link" CausesValidation="false"
														OnClick="btntsicancel_Click" />
												</div>
											</td>
										</tr>
									</table>
								</asp:Panel>
								<asp:Panel ID="pnlPlotMapping" runat="server">
									<table width="100%" border="0" cellspacing="0" cellpadding="0" class="form_container_td">
										<tr>
											<td class="field_title" width="5%">
												<asp:Label ID="Label8" Text="CUSTOMER NAME" runat="server"></asp:Label>
											</td>
											<td class="field_input" width="25%">
												<asp:TextBox ID="txtplotcustomername" runat="server" CssClass="large" Enabled="false"
													Width="220"></asp:TextBox>
												<asp:Label ID="Label9" Text="CITY NAME" runat="server"></asp:Label>
												<asp:TextBox ID="city" runat="server" Text="howrah" Width="100"></asp:TextBox>
												<asp:TextBox ID="lat" runat="server" Text="" Style="display: none;"></asp:TextBox><asp:TextBox
													ID="lng" runat="server" Text="" Style="display: none;"></asp:TextBox>
											</td>
										</tr>
										<tr>
											<td colspan="2">
												<input type="button" value="Geocode" onclick="initialize()">
												<div id="map_canvas" style="width: 100%; height: 500px;">
												</div>
												<script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=false"></script>
												<script type="text/javascript">
													function codeAddress() {
														var city = document.getElementById("city").value;
														var geocoder = new google.maps.Geocoder();

														geocoder.geocode({ 'city': city }, function (results, status) {
															var location = results[0].geometry.location;
															var lat = location.lat();
															var lng = location.lng();

															document.getElementById('lat').value = lat;
															document.getElementById('lng').value = lng;
														});
													}
												</script>
												<script type="text/javascript">
													function initialize() {
														codeAddress();
														var lat = document.getElementById('lat').value;
														var lng = document.getElementById('lng').value;
														var myLatlng = new google.maps.LatLng(lat, lng);
														var myOptions = {
															zoom: 13,
															center: myLatlng,
															mapTypeId: google.maps.MapTypeId.ROADMAP,

														}
														var map = new google.maps.Map(document.getElementById("map_canvas"), myOptions);

														marker = new google.maps.Marker({
															position: myLatlng,
															map: map,
															title: 'Default Marker',
															draggable: true
														});

														google.maps.event.addListener(map, 'click', function (event) {

															marker = new google.maps.Marker({
																position: event.latLng,
																map: map,
																title: 'Click Generated Marker',
																draggable: true
															});
														}
														);

														google.maps.event.addListener(
															marker,
															'drag',
															function (event) {
																document.getElementById('lat').value = this.position.lat();
																document.getElementById('lng').value = this.position.lng();
																//alert('drag');
															});


														google.maps.event.addListener(marker, 'dragend', function (event) {
															document.getElementById('lat').value = this.position.lat();
															document.getElementById('lng').value = this.position.lng();
															//alert('Drag end');
														});


													}
												</script>
											</td>
										</tr>
										<tr>
											<td align="left"></td>
											<td align="left" style="padding: 8px 0;">
												<div class="btn_24_blue">
													<span class="icon disk_co"></span>
													<asp:Button ID="btnplotsubmit" runat="server" Text="Save" CssClass="btn_link" CausesValidation="false"
														OnClick="btnplotsubmit_Click" />
												</div>
												&nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_blue">
													<span class="icon cross_octagon_co"></span>
													<asp:Button ID="btnplotcancel" runat="server" Text="Cancel" CssClass="btn_link" CausesValidation="false"
														OnClick="btnplotcancel_Click" />
												</div>
											</td>
										</tr>
									</table>
								</asp:Panel>
								<asp:Panel ID="PnlAddress" runat="server">
									<table width="100%" border="0" cellspacing="0" cellpadding="0" class="form_container_td">
										<tr>
											<td width="120" class="field_title">
												<asp:Label ID="Label21" Text="CUSTOMER NAME" runat="server"></asp:Label>
											</td>
											<td class="field_input">
												<asp:TextBox ID="txtcustomer" runat="server" CssClass="large" Enabled="false"></asp:TextBox>
											</td>
										</tr>
										<tr>
											<td colspan="2" class="field_input" style="padding-left: 10px;">
												<fieldset>
													<legend>ADDRESS MAPPING</legend>
													<table border="0" cellspacing="0" cellpadding="0" class="form_container_td">
														<tr>
															<td width="60" class="field_title">
																<asp:Label ID="Label22" Text="Address" runat="server"></asp:Label>&nbsp;<span class="req">*</span>
															</td>
															<td width="238" class="field_input">
																<asp:TextBox ID="txtmapaddress" runat="server" CssClass=" large" TextMode="MultiLine"
																	MaxLength="255" ValidationGroup="ADDRESS"></asp:TextBox>
																<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="ADDRESS"
																	ControlToValidate="txtmapaddress" Display="None" ErrorMessage="Required!" SetFocusOnError="true"
																	ValidateEmptyText="false"> </asp:RequiredFieldValidator>
																<ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender13" runat="server"
																	HighlightCssClass="errormassage" PopupPosition="BottomLeft" TargetControlID="RequiredFieldValidator7"
																	WarningIconImageUrl="../images/050.png">
																</ajaxToolkit:ValidatorCalloutExtender>
															</td>
															<td width="80" class="field_title">
																<asp:Label ID="Label23" Text="PinCode" runat="server"></asp:Label>&nbsp;<span class="req">*</span>
															</td>
															<td class="field_input">
																<asp:TextBox ID="txtmappin" runat="server" CssClass="small" MaxLength="6" ValidationGroup="ADDRESS"
																	onfocus="disableautocompletion(this.id);" onkeypress="return isNumberKey(event);"></asp:TextBox>
																<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="ADDRESS"
																	ControlToValidate="txtmappin" Display="None" ErrorMessage="Required!" SetFocusOnError="true"
																	ValidateEmptyText="false"> </asp:RequiredFieldValidator>
																<ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server"
																	HighlightCssClass="errormassage" PopupPosition="BottomLeft" TargetControlID="RequiredFieldValidator8"
																	WarningIconImageUrl="../images/050.png">
																</ajaxToolkit:ValidatorCalloutExtender>
																&nbsp; &nbsp; &nbsp; &nbsp;
                                                                <asp:Button ID="btnadd" runat="server" ValidationGroup="ADDRESS" OnClick="btnadd_Click"
																	Text="" CssClass="addbtn_blue" />
															</td>
														</tr>
														<tr>
															<td class="field_input" colspan="4">
																<div class="gridcontent-short">
																	<li>
																		<cc1:Grid ID="grdaddress" runat="server" Serialize="true" AllowAddingRecords="false"
																			AutoGenerateColumns="false" AllowSorting="true" AllowPageSizeSelection="true"
																			FolderStyle="../GridStyles/premiere_blue" AllowPaging="false" PageSize="100">
																			<Columns>
																				<cc1:Column ID="Column31" DataField="SlNo" HeaderText="SL" runat="server" Width="60px">
																					<TemplateSettings TemplateId="slnoAddress" />
																				</cc1:Column>
																				<cc1:Column ID="Column32" DataField="GUID" HeaderText="GUID" runat="server" Visible="false" />
																				<cc1:Column ID="Column33" DataField="CUSTOMERID" HeaderText="CUSTOMERID" Visible="false"
																					runat="server">
																				</cc1:Column>
																				<cc1:Column ID="Column34" DataField="CUSTOMERNAME" HeaderText="CUSTOMERNAME" Width="50"
																					Visible="false" runat="server">
																				</cc1:Column>
																				<cc1:Column ID="Column35" DataField="ADDRESS" HeaderText="ADDRESS" Width="800px"
																					runat="server">
																				</cc1:Column>
																				<cc1:Column ID="Column36" DataField="PINCODE" HeaderText="PINCODE" Width="150px"
																					runat="server">
																				</cc1:Column>
																				<cc1:Column HeaderText="Delete" AllowEdit="false" AllowDelete="true" Width="80px">
																					<TemplateSettings TemplateId="deleteBtnAddress" />
																				</cc1:Column>
																			</Columns>
																			<Templates>
																				<cc1:GridTemplate runat="server" ID="deleteBtnAddress">
																					<Template>
																						<a href="javascript: //" class="action-icons c-delete" id="btnGridDelete_<%# Container.PageRecordIndex %>"
																							onclick="AddressMappingDeleteMethod(this)"></a>
																					</Template>
																				</cc1:GridTemplate>
																				<cc1:GridTemplate runat="server" ID="slnoAddress">
																					<Template>
																						<asp:Label ID="lblslno" runat="server" Text='<%#Container.PageRecordIndex+1 %>'></asp:Label>
																					</Template>
																				</cc1:GridTemplate>
																			</Templates>
																			<ScrollingSettings ScrollWidth="90%" ScrollHeight="180" />
																		</cc1:Grid>
																		<asp:Button ID="btndelete" runat="server" CausesValidation="false" Text="grddelete"
																			Style="display: none" OnClick="btndelete_Click" OnClientClick="return confirm('Are you sure you want to delete this record?');" />
																		<asp:HiddenField ID="HdnGrdDel" runat="server" />
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
													<asp:Button ID="btnAddressSubmit" runat="server" Text="Save" CssClass="btn_link"
														CausesValidation="false" OnClick="btnAddressSubmit_Click" />
												</div>
												&nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_blue">
													<span class="icon cross_octagon_co"></span>
													<asp:Button ID="btnAddressCancel" runat="server" Text="Cancel" CssClass="btn_link"
														CausesValidation="false" OnClick="btnAddressCancel_Click" />
												</div>
											</td>
										</tr>
									</table>
								</asp:Panel>
								<asp:Panel ID="pnlDisplay" runat="server">
									<tr>
										<td>
											<table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
												<tr>
													<td class="innerfield_title" width="150px" style="padding-left: 12px">
														<asp:Label ID="Label19" Text="Business Segment" runat="server"></asp:Label>
													</td>
													<td width="240" class="innerfield_title">
														<asp:ListBox ID="ddlsearchbs" runat="server" SelectionMode="Multiple" TabIndex="4"
															Width="200%" AppendDataBoundItems="True" ValidationGroup="Show"></asp:ListBox>
														
														<asp:CustomValidator ID="CustomValidator1" runat="server" ValidateEmptyText="true"
															ControlToValidate="ddlsearchbs" ValidationGroup="Show" ErrorMessage="Required"
															Font-Bold="true" Display="Dynamic" ClientValidationFunction="ValidateListBoxddlsearchbs"
															ForeColor="Red"></asp:CustomValidator>
													</td>
													<td class="innerfield_title" width="80px" style="padding-left: 12px">
														<asp:Label ID="Label31" Text="Depot" runat="server"></asp:Label>
													</td>
													<td width="180" class="innerfield_title">
														<asp:ListBox ID="ddldepots" runat="server" SelectionMode="Multiple" TabIndex="4"
															AppendDataBoundItems="True" ValidationGroup="Show"></asp:ListBox>
														<asp:CustomValidator ID="CustomValidator2" runat="server" ValidateEmptyText="true"
															ControlToValidate="ddldepots" ValidationGroup="Show" ErrorMessage="Required"
															Font-Bold="true" Display="Dynamic" ClientValidationFunction="ValidateListBoxddldepots"
															ForeColor="Red"></asp:CustomValidator>
													</td>
													<td class="innerfield_title" width="80px" style="padding-left: 12px">
														<asp:Label ID="Label34" Text="Customer Type" runat="server" ValidationGroup="Show"></asp:Label>
													</td>
													<td width="180" class="innerfield_title">
														<asp:ListBox ID="ddlcustype" runat="server" SelectionMode="Multiple" TabIndex="4"
															AppendDataBoundItems="True" ValidationGroup="Show"></asp:ListBox>
														<asp:CustomValidator ID="CustomValidator3" runat="server" ValidateEmptyText="true"
															ControlToValidate="ddldepots" ValidationGroup="Show" ErrorMessage="Required"
															Font-Bold="true" Display="Dynamic" ClientValidationFunction="ValidateListBoxddlcustype"
															ForeColor="Red"></asp:CustomValidator>
													</td>
												</tr>
												<tr>
												<tr>
													<td class="innerfield_title" width="80px" style="padding-left: 12px">
														<asp:Label ID="Label35" Text="Accounts Group" runat="server"></asp:Label>
													</td>
													<td width="180" class="innerfield_title">
														<asp:ListBox ID="ddlaccountsgroup" runat="server" SelectionMode="Multiple" TabIndex="4"
															AppendDataBoundItems="True" ValidationGroup="Show"></asp:ListBox>
														<asp:CustomValidator ID="CustomValidator4" runat="server" ValidateEmptyText="true"
															ControlToValidate="ddldepots" ValidationGroup="Show" ErrorMessage="Required"
															Font-Bold="true" Display="Dynamic" ClientValidationFunction="ValidateListBoxddlaccountsgroup"
															ForeColor="Red"></asp:CustomValidator>
													</td>
													<td width="105" class="field_input">
														<div class="btn_24_blue">
															<span class="icon exclamation_co"></span>
															<asp:Button ID="btnShow" runat="server" Text="Search" CssClass="btn_link" OnClick="btnShow_Click"
																ValidationGroup="Show" />
														</div>
													</td>
													<td class="field_input" id="tdExcel" runat="server">
														<div class="btn_24_blue">
															<span class="icon doc_excel_table_co"></span><a href="#" onclick="exportToExcel();">
																<asp:Button ID="btnExport" runat="server" Text="Export Excel" CssClass="btn_link" /></a>
														</div>
													</td>
													<td class="field_input" style="display: none" colspan="3">
														<div class="btn_24_blue">
															<span class="icon doc_excel_table_co"></span>
															<asp:Button ID="btngeneraltemp" runat="server" Text="Generate Template" CssClass="btn_link"
																CausesValidation="false" OnClick="btngeneraltemp_Click" />
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
											<input name="" id="OboutButton2" type="submit" value="" class="search_btn" onclick="return performSearch();"
												style="height: 28px;">
										</li>
									</ul>
									<div class="gridcontent">
										<cc1:Grid ID="gvCustomer" runat="server" CallbackMode="true" Serialize="true" FolderStyle="../GridStyles/premiere_blue"
											EnableRecordHover="true" AutoGenerateColumns="false" AllowPageSizeSelection="true"
											OnExported="gvCustomer_Exported" OnExporting="gvCustomer_Exporting" OnDeleteCommand="DeleteRecord"
											AllowPaging="true" AllowAddingRecords="false" PageSize="100" AllowFiltering="true"
											OnRowDataBound="gvCustomer_RowDataBound">
											<ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" OnClientDelete="OnDelete" />
											<ExportingSettings ExportColumnsFooter="true" ExportGroupFooter="true" ExportGroupHeader="true"
												FileName="CustomerDetails" ExportAllPages="true" ExportDetails="true" AppendTimeStamp="false" />
											<CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size: 10px;background-color: #DCE3E8;color: #0000FF;"
												CSSExportCellStyle="font-weight: normal;font-size: 10px;color: #0000;background-color: AFEEEE" />
											<FilteringSettings InitialState="Visible" />
											<Columns>
												<cc1:Column ID="Column11" DataField="SLNO" HeaderText="SLNO" runat="server" Width="60">
												</cc1:Column>
												<cc1:Column ID="Column10" DataField="CUSTOMERID" ReadOnly="true" HeaderText="CUSTOMERID"
													runat="server" Visible="false">
												</cc1:Column>
												<cc1:Column ID="Column12" DataField="CUSTOMERNAME" HeaderText="CUSTOMER NAME" runat="server"
													Width="180" Wrap="true">
													<FilterOptions>
														<cc1:FilterOption Type="NoFilter" />
														<cc1:FilterOption Type="Contains" />
														<cc1:FilterOption Type="StartsWith" />
													</FilterOptions>
												</cc1:Column>
												<cc1:Column ID="Column47" DataField="DEPOTNAME" HeaderText="DEPOT" runat="server"
													Width="180" Wrap="true">
													<FilterOptions>
														<cc1:FilterOption Type="NoFilter" />
														<cc1:FilterOption Type="Contains" />
														<cc1:FilterOption Type="StartsWith" />
													</FilterOptions>
												</cc1:Column>
												<cc1:Column ID="Column43" DataField="GSTNO" HeaderText="GST NO" runat="server" Width="150"
													Wrap="true">
													<FilterOptions>
														<cc1:FilterOption Type="NoFilter" />
														<cc1:FilterOption Type="Contains" />
														<cc1:FilterOption Type="StartsWith" />
													</FilterOptions>
												</cc1:Column>
												<cc1:Column ID="Column13" DataField="CUSTYPE_NAME" HeaderText="CUTOMER TYPE" runat="server"
													Width="170" Wrap="true">
													<FilterOptions>
														<cc1:FilterOption Type="NoFilter" />
														<cc1:FilterOption Type="Contains" />
														<cc1:FilterOption Type="StartsWith" />
													</FilterOptions>
												</cc1:Column>
												<cc1:Column ID="Column14" DataField="CODE" HeaderText="CUSTOMER CODE" runat="server"
													Visible="true" Width="140" Wrap="true">
													<FilterOptions>
														<cc1:FilterOption Type="NoFilter" />
														<cc1:FilterOption Type="Contains" />
														<cc1:FilterOption Type="StartsWith" />
													</FilterOptions>
												</cc1:Column>
												<cc1:Column ID="Column15" DataField="BUSINESSSEGMENTNAME" HeaderText="BUSINESS SEGMENT"
													runat="server" Width="190" Wrap="true" Visible="true">
													<FilterOptions>
														<cc1:FilterOption Type="NoFilter" />
														<cc1:FilterOption Type="Contains" />
														<cc1:FilterOption Type="StartsWith" />
													</FilterOptions>
												</cc1:Column>
												<cc1:Column ID="Column16" DataField="GROUPNAME" HeaderText="GROUP" runat="server"
													Width="140" Wrap="true">
													<FilterOptions>
														<cc1:FilterOption Type="NoFilter" />
														<cc1:FilterOption Type="Contains" />
														<cc1:FilterOption Type="StartsWith" />
													</FilterOptions>
												</cc1:Column>
												<cc1:Column ID="Column20" DataField="ADDRESS" HeaderText="ADDRESS" runat="server"
													Width="140" Wrap="true" Visible="false">
													<FilterOptions>
														<cc1:FilterOption Type="NoFilter" />
														<cc1:FilterOption Type="Contains" />
														<cc1:FilterOption Type="StartsWith" />
													</FilterOptions>
												</cc1:Column>
												<cc1:Column ID="Column22" DataField="PIN" HeaderText="PIN" runat="server" Width="140"
													Visible="false" Wrap="true">
													<FilterOptions>
														<cc1:FilterOption Type="NoFilter" />
														<cc1:FilterOption Type="Contains" />
														<cc1:FilterOption Type="StartsWith" />
													</FilterOptions>
												</cc1:Column>
												<cc1:Column ID="Column27" DataField="ADDRESS" HeaderText="ADDRESS" runat="server"
													Width="140" Visible="false">
													<FilterOptions>
														<cc1:FilterOption Type="NoFilter" />
														<cc1:FilterOption Type="Contains" />
														<cc1:FilterOption Type="StartsWith" />
													</FilterOptions>
												</cc1:Column>
												<cc1:Column ID="Column28" DataField="STATENAME" HeaderText="STATE" runat="server"
													Width="140" Wrap="true">
													<FilterOptions>
														<cc1:FilterOption Type="NoFilter" />
														<cc1:FilterOption Type="Contains" />
														<cc1:FilterOption Type="StartsWith" />
													</FilterOptions>
												</cc1:Column>
												<cc1:Column ID="Column29" DataField="DISTRICTNAME" HeaderText="DISTRICT" runat="server"
													Visible="true" Width="140" Wrap="true">
													<FilterOptions>
														<cc1:FilterOption Type="NoFilter" />
														<cc1:FilterOption Type="Contains" />
														<cc1:FilterOption Type="StartsWith" />
													</FilterOptions>
												</cc1:Column>
												<cc1:Column ID="Column21" DataField="CITYNAME" HeaderText="CITY" runat="server" Width="140"
													Wrap="true">
													<FilterOptions>
														<cc1:FilterOption Type="NoFilter" />
														<cc1:FilterOption Type="Contains" />
														<cc1:FilterOption Type="StartsWith" />
													</FilterOptions>
												</cc1:Column>
												<cc1:Column ID="Column23" DataField="MOBILE1" HeaderText="MOBILE" runat="server"
													Width="140" Wrap="true">
													<FilterOptions>
														<cc1:FilterOption Type="NoFilter" />
														<cc1:FilterOption Type="Contains" />
														<cc1:FilterOption Type="StartsWith" />
													</FilterOptions>
												</cc1:Column>
												<cc1:Column ID="Column24" DataField="CONTACTPERSON1" HeaderText="CONTACTPERSON" runat="server"
													Visible="false" Width="140" Wrap="true">
													<FilterOptions>
														<cc1:FilterOption Type="NoFilter" />
														<cc1:FilterOption Type="Contains" />
														<cc1:FilterOption Type="StartsWith" />
													</FilterOptions>
												</cc1:Column>
												<cc1:Column ID="Column25" DataField="PANCARDNO" HeaderText="PAN NO." runat="server"
													Visible="false" Width="140" Wrap="true">
													<FilterOptions>
														<cc1:FilterOption Type="NoFilter" />
														<cc1:FilterOption Type="Contains" />
														<cc1:FilterOption Type="StartsWith" />
													</FilterOptions>
												</cc1:Column>
												<cc1:Column ID="Column45" DataField="EMAILID1" HeaderText="E-MAIL" runat="server"
													Visible="false" Width="140" Wrap="true">
													<FilterOptions>
														<cc1:FilterOption Type="NoFilter" />
														<cc1:FilterOption Type="Contains" />
														<cc1:FilterOption Type="StartsWith" />
													</FilterOptions>
												</cc1:Column>
												<cc1:Column ID="Column46" DataField="ACCGROUPNAME" HeaderText="ACC. GROUP" runat="server"
													Visible="false" Width="140" Wrap="true">
													<FilterOptions>
														<cc1:FilterOption Type="NoFilter" />
														<cc1:FilterOption Type="Contains" />
														<cc1:FilterOption Type="StartsWith" />
													</FilterOptions>
												</cc1:Column>
												<%--<cc1:Column ID="Column26" DataField="TINNO" HeaderText="TIN NO." runat="server" Width="140"
													Wrap="true" Visible="false">
													<FilterOptions>
														<cc1:FilterOption Type="NoFilter" />
														<cc1:FilterOption Type="Contains" />
														<cc1:FilterOption Type="StartsWith" />
													</FilterOptions>
												</cc1:Column>--%>
												<cc1:Column ID="Column17" DataField="ISACTIVE" HeaderText="STATUS" runat="server"
													Width="90" Wrap="true">
													<FilterOptions>
														<cc1:FilterOption Type="NoFilter" />
														<cc1:FilterOption Type="Contains" />
													</FilterOptions>
												</cc1:Column>
												<%--<cc1:Column ID="Column48" DataField="GENCODE" HeaderText="BANKCODE"
													runat="server" Width="100" Wrap="true" visible="false">
													<FilterOptions>
														<cc1:FilterOption Type="NoFilter" />
														<cc1:FilterOption Type="Contains" />
														<cc1:FilterOption Type="StartsWith" />
													</FilterOptions>
												</cc1:Column>--%>
												<cc1:Column ID="Column18" AllowEdit="true" AllowDelete="true" HeaderText="EDIT" runat="server"
													Wrap="true" Width="70">
													<TemplateSettings TemplateId="editBtnTemplate" />
												</cc1:Column>
												<%--<cc1:Column HeaderText="DELETE" AllowEdit="false" AllowDelete="true" Width="80" Visible="false" >
													<TemplateSettings TemplateId="deleteBtnTemplate" />
												</cc1:Column>--%>
												<cc1:Column HeaderText="DEPOT MAPPING" AllowEdit="false" AllowDelete="true" Width="70"
													Wrap="true" Visible="false">
													<TemplateSettings TemplateId="CustDepotMapping" />
												</cc1:Column>
												<cc1:Column HeaderText="PRODUCT MAPPING" AllowEdit="false" AllowDelete="true" Width="80"
													Visible="true" Wrap="true">
													<TemplateSettings TemplateId="ProductMapping" />
												</cc1:Column>
												<cc1:Column HeaderText="Transporter Mapping" AllowEdit="false" AllowDelete="true"
													Width="70" Visible="true" Wrap="true">
													<TemplateSettings TemplateId="TransporterMapping" />
												</cc1:Column>
												<cc1:Column HeaderText="SALESFORCE MAPPING" AllowEdit="false" AllowDelete="true"
													Width="90" Wrap="true">
													<TemplateSettings TemplateId="TSIMapping" />
												</cc1:Column>
												<cc1:Column HeaderText="PLOT MAPPING" AllowEdit="false" AllowDelete="true" Width="70"
													Wrap="true">
													<TemplateSettings TemplateId="PLOTMapping" />
												</cc1:Column>
												<cc1:Column HeaderText="ADDRESS MAPPING" AllowEdit="false" AllowDelete="true" Width="80"
													Wrap="true">
													<TemplateSettings TemplateId="AddressMapping" />
												</cc1:Column>
												<cc1:Column HeaderText="VIEW" AllowEdit="false" AllowDelete="true" Width="80" Wrap="true">
													<TemplateSettings TemplateId="viewBtnTemplate" />
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
												<cc1:GridTemplate runat="server" ID="viewBtnTemplate">
													<Template>
														<a href="javascript: //" class="h_icon zoom_sl" id="btnGridEdit_<%# Container.PageRecordIndex %>"
															onclick="CallServerViewMethod(this)"></a>
													</Template>
												</cc1:GridTemplate>
												<cc1:GridTemplate runat="server" ID="GridTemplate2">
													<Template>
														<asp:Label ID="lblslno" runat="server" Text='<%#Container.PageRecordIndex+1 %>'></asp:Label>
													</Template>
												</cc1:GridTemplate>
											</Templates>
											<Templates>
												<cc1:GridTemplate runat="server" ID="deleteBtnTemplate">
													<Template>
														<a href="javascript: //" class="action-icons c-delete" title="Delete" onclick="gvCustomer.delete_record(this)"></a>
													</Template>
												</cc1:GridTemplate>
											</Templates>
											<Templates>
												<cc1:GridTemplate runat="server" ID="CustDepotMapping">
													<Template>
														<a href="javascript: //" class="h_icon map" title="Depot Mapping" onclick="openCustDepot('<%# Container.DataItem["CUSTOMERNAME"] %>','<%# Container.DataItem["CUSTOMERID"] %>','<%# Container.DataItem["CUSTYPE_NAME"] %>')" />
													</Template>
												</cc1:GridTemplate>
											</Templates>
											<Templates>
												<cc1:GridTemplate runat="server" ID="ProductMapping">
													<Template>
														<a href="javascript: //" class="h_icon glyphicon-apple" title="Product Mapping" onclick="openProduct('<%# Container.DataItem["CUSTOMERNAME"] %>','<%# Container.DataItem["CUSTOMERID"] %>')" />
													</Template>
												</cc1:GridTemplate>
											</Templates>
											<Templates>
												<cc1:GridTemplate runat="server" ID="TransporterMapping">
													<Template>
														<a href="javascript: //" class="h_icon glyphicon-apple" title="Transporter Mapping"
															onclick="openTransporter('<%# Container.DataItem["CUSTOMERNAME"] %>','<%# Container.DataItem["CUSTOMERID"] %>','<%# Container.DataItem["CUSTYPE_ID"] %>')" />
													</Template>
												</cc1:GridTemplate>
											</Templates>
											<Templates>
												<cc1:GridTemplate runat="server" ID="TSIMapping">
													<Template>
														<a href="javascript: //" class="h_icon zoom_sl" title="TSI Mapping" onclick="openTSIMapping('<%# Container.DataItem["CUSTOMERNAME"] %>','<%# Container.DataItem["CUSTOMERID"] %>')" />
													</Template>
												</cc1:GridTemplate>
											</Templates>
											<Templates>
												<cc1:GridTemplate runat="server" ID="PLOTMapping">
													<Template>
														<a href="javascript: //" class="h_icon youtube" title="PLOT Mapping" onclick="openPLOTMapping('<%# Container.DataItem["CUSTOMERNAME"] %>','<%# Container.DataItem["CUSTOMERID"] %>','<%# Container.DataItem["CITYNAME"] %>')" />
													</Template>
												</cc1:GridTemplate>
											</Templates>
											<Templates>
												<cc1:GridTemplate runat="server" ID="AddressMapping">
													<Template>
														<a href="javascript: //" class="h_icon youtube" title="Address Mapping" onclick="openAddressmapping('<%# Container.DataItem["CUSTOMERNAME"] %>','<%# Container.DataItem["CUSTOMERID"] %>')" />
													</Template>
												</cc1:GridTemplate>
											</Templates>
											<ScrollingSettings ScrollWidth="100%" ScrollHeight="330" NumberOfFixedColumns="3" />
										</cc1:Grid>
										<asp:Button ID="btngridsave" runat="server" Text="GridSave" Style="display: none"
											OnClick="btngridsave_Click" CausesValidation="false" />
										<asp:Button ID="btncustDepotMapping" runat="server" Text="Mapping" Style="display: none"
											OnClick="btncustDepotMapping_Click" CausesValidation="false" />
										<asp:Button ID="btnProductMapping" runat="server" Text="Mapping" Style="display: none"
											OnClick="btnProductMapping_Click" CausesValidation="false" />
										<asp:Button ID="btntsimapping" runat="server" Text="TSI Mapping" Style="display: none"
											OnClick="btntsimapping_Click" CausesValidation="false" />
										<asp:Button ID="btnplotmapping" runat="server" Text="PLOT Mapping" Style="display: none"
											OnClick="btnplotmapping_Click" CausesValidation="false" />
										<asp:Button ID="btnaddressmapping" runat="server" Text="PLOT Mapping" Style="display: none"
											OnClick="btnaddressmapping_Click" CausesValidation="false" />
										<asp:Button ID="btnTransporter" runat="server" Text="PLOT Mapping" Style="display: none"
											OnClick="btnTransporter_Click" CausesValidation="false" />
										<asp:Button ID="btnview" runat="server" Text="View" Style="display: none" OnClick="btnview_Click"
											CausesValidation="false" />
									</div>
								</asp:Panel>
							</div>
						</div>
					</div>
					<cc1:MessageBox ID="MessageBox1" runat="server" />

					<span class="clear"></span>
				</div>
				<span class="clear"></span>
				<link href="../css/bootstrap-multiselect.css" rel="stylesheet" />
				<script type="text/javascript" src="../js/bootstrap.js"></script>
				<script type="text/javascript" src="../js/bootstrap-multiselect.js"></script>
				<script type="text/javascript">
					window.onload = function () {
						oboutGrid.prototype.selectRecordByClick = function () {
							return;
						}
						oboutGrid.prototype.showSelectionArea = function () {
							return;
						}
					}

					function isNumberKey(evt) {
						var charCode = (evt.which) ? evt.which : event.keyCode;
						if ((charCode < 48 || charCode > 57) && (charCode != 8))
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

					function isNumberKeyWithslash(evt) {
						var charCode = (evt.which) ? evt.which : event.keyCode;
						if ((charCode < 48 || charCode > 57) && (charCode != 8) && (charCode != 47))
							return false;

						return true;
					}
				</script>
				<script type="text/javascript">
					$(function () {
						$('#ContentPlaceHolder1_ddlsearchbs').multiselect({
							includeSelectAllOption: true
						});
					});
				</script>
				<script type="text/javascript">
					$(function () {
						$('#ContentPlaceHolder1_ddldepots').multiselect({
							includeSelectAllOption: true
						});
					});
				</script>
				<script type="text/javascript">
					$(function () {
						$('#ContentPlaceHolder1_ddlaccountsgroup').multiselect({
							includeSelectAllOption: true
						});
					});
				</script>
				<script type="text/javascript">
					$(function () {
						$('#ContentPlaceHolder1_ddlcustype').multiselect({
							includeSelectAllOption: true
						});
					});
				</script>
				<script type="text/javascript">
					function 
						
						() {
						var checkedValue = document.getElementById("<%= chkgst.ClientID%>");
						if (checkedValue.checked == true) {
							var panno = document.getElementById("<%=txtPanNO.ClientID %>").value;
							document.getElementById("<%=txtgstpanno.ClientID %>").value = panno;
						}
					}
				</script>
				<script type="text/javascript">
					function printname() {
						var customername = document.getElementById("<%=txtName.ClientID %>").value;
						document.getElementById("<%=txtprintname.ClientID %>").value = customername;

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
					function Mobno() {
						var args = document.getElementById("txtMobileNo").value;
						if (args.length < 10 || args.length > 10) {

							args.IsValid = false
						}
						else {

							args.IsValid = true
						}
					}
				</script>
				<script type="text/javascript">
					function exportToExcel() {
						gvCustomer.exportToExcel();
					}
				</script>
				<script type="text/javascript">
					function OnBeforeDelete(record) {
						record.Error = '';
						document.getElementById("<%=Hdn_Fld.ClientID %>").value = record.CUSTOMERID;
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
					function CallProductDeleteMethod(oLink) {
						var iRecordIndex = oLink.id.toString().replace("btnGridDelete_", "");
						document.getElementById("<%=hdnproductdelete.ClientID %>").value = gvProductadd.Rows[iRecordIndex].Cells[1].Value;
						document.getElementById("<%=btnproductdelete.ClientID %>").click();

					}
					function CallTransportDeleteMethod(oLink) {
						var iRecordIndex = oLink.id.toString().replace("btnGridDelete_", "");
						document.getElementById("<%=Hdn_Fld_Delete.ClientID %>").value = grdFinalGrid.Rows[iRecordIndex].Cells[1].Value;
						document.getElementById("<%=btntransporterdelete.ClientID %>").click();

					}
					function AddressMappingDeleteMethod(oLink) {
						var iRecordIndex = oLink.id.toString().replace("btnGridDelete_", "");
						document.getElementById("<%=HdnGrdDel.ClientID %>").value = grdaddress.Rows[iRecordIndex].Cells[1].Value;
						document.getElementById("<%=btndelete.ClientID %>").click();

					}

					function disableautocompletion(id) {
						var passwordControl = document.getElementById(id);
						passwordControl.setAttribute("autocomplete", "off");
					}
				</script>
				<script type="text/javascript">
					function CallServerMethod(oLink) {
						var iRecordIndex = oLink.id.toString().replace("btnGridEdit_", "");
						document.getElementById("<%=Hdn_Fld.ClientID %>").value = gvCustomer.Rows[iRecordIndex].Cells[1].Value;
						document.getElementById("<%=Hdn_controltagE.ClientID %>").value = "E";
						document.getElementById("<%=btngridsave.ClientID %>").click();
					}
				</script>
				<script type="text/javascript">
					function CallServerViewMethod(oLink) {
						var iRecordIndex = oLink.id.toString().replace("btnGridEdit_", "");
						document.getElementById("<%=Hdn_Fld.ClientID %>").value = gvCustomer.Rows[iRecordIndex].Cells[1].Value;
						document.getElementById("<%=Hdn_controltagD.ClientID %>").value = "D";
						document.getElementById("<%=btnview.ClientID %>").click();
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
						gvCustomer.addFilterCriteria('CUSTYPE_NAME', OboutGridFilterCriteria.Contains, searchValue);
						gvCustomer.addFilterCriteria('CODE', OboutGridFilterCriteria.Contains, searchValue);
						gvCustomer.addFilterCriteria('CUSTOMERNAME', OboutGridFilterCriteria.Contains, searchValue);
						gvCustomer.addFilterCriteria('BUSINESSSEGMENTNAME', OboutGridFilterCriteria.Contains, searchValue);
						gvCustomer.addFilterCriteria('GROUPNAME', OboutGridFilterCriteria.Contains, searchValue);
						gvCustomer.addFilterCriteria('ISACTIVE', OboutGridFilterCriteria.Contains, searchValue);

						gvCustomer.executeFilter();
						searchTimeout = null;
						return false;
					}
					function openCustDepot(Custname, Custid, CustType) {
						document.getElementById("<%=Hdn_Fld.ClientID %>").value = Custid;
						document.getElementById("<%=txtCustomername.ClientID %>").value = Custname;
						document.getElementById("<%=hdnCustCategory.ClientID %>").value = CustType;
						document.getElementById("<%=btncustDepotMapping.ClientID %>").click();
					}
					function openProduct(Custname, Custid) {
						document.getElementById("<%=Hdn_Fld.ClientID %>").value = Custid;
						document.getElementById("<%=txtCustomerproductname.ClientID %>").value = Custname;
						document.getElementById("<%=btnProductMapping.ClientID %>").click();
					}
					function openTransporter(Custname, Custid, CustType) {
						document.getElementById("<%=txtDistrbutor.ClientID %>").value = Custname;
						document.getElementById("<%=Hdn_Fld.ClientID %>").value = Custid;
						document.getElementById("<%=Hdncustid.ClientID %>").value = CustType;
						document.getElementById("<%=btnTransporter.ClientID %>").click();
					}
					function openAddressmapping(Custname, Custid) {
						document.getElementById("<%=Hdn_Fld.ClientID %>").value = Custid;
						document.getElementById("<%=txtcustomer.ClientID %>").value = Custname;
						document.getElementById("<%=btnaddressmapping.ClientID %>").click();
					}
					function openTSIMapping(Custname, Custid) {
						document.getElementById("<%=Hdn_Fld.ClientID %>").value = Custid;
						document.getElementById("<%=txttsicustomername.ClientID %>").value = Custname;
						document.getElementById("<%=btntsimapping.ClientID %>").click();
					}

					function openPLOTMapping(Custname, Custid, cityname) {
						document.getElementById("<%=Hdn_Fld.ClientID %>").value = Custid;
						document.getElementById("<%=hdn_city.ClientID %>").value = cityname;
						document.getElementById("<%=txtplotcustomername.ClientID %>").value = Custname;
						document.getElementById("<%=hdncustomername.ClientID %>").value = Custname;
						document.getElementById("<%=btnplotmapping.ClientID %>").click();
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
                <script type="text/javascript" language="javascript">
                function CheckAllheader(Checkbox) {
                    var GridVwHeaderCheckbox = document.getElementById("<%=gvBrandTypeMap.ClientID %>");
                    var totalcount = 0;
                    if (Checkbox.checked) {
                        for (i = 1; i < GridVwHeaderCheckbox.rows.length; i++) {
                            GridVwHeaderCheckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
                            totalcount = totalcount + 1;
                        }
                    }
                    else {
                        for (i = 1; i < GridVwHeaderCheckbox.rows.length; i++) {
                            GridVwHeaderCheckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = false;
                            
                        }
                    }
                    if (totalcount > 0) {
                        lbltotalcount.style.display = "";
                        lbltotalcount.innerHTML = 'You have selected : ' + totalcount + ' Product(s).';
                    }
                    else {
                        lbltotalcount.style.display = "none";
                    }
                }
            </script>
            <script type="text/javascript">
                function setrowcolor(chb) {
                    if (chb.checked) {
                    }
                    else {
                        
                    }

                    var GridVwHeaderCheckbox = document.getElementById("<%=gvBrandTypeMap.ClientID %>");
                    var lbltotalcount = document.getElementById("<%=lbltotalcount.ClientID %>");
                    var totalcount = 0;

                    for (i = 1; i < GridVwHeaderCheckbox.rows.length; i++) {
                        if (GridVwHeaderCheckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked == true) {
                            totalcount = totalcount + 1;
                        }
                    }
                    if (totalcount > 0) {
                        lbltotalcount.style.display = "";
                        lbltotalcount.innerHTML = 'You have selected : ' + totalcount + ' Product(s).';
                    }
                    else {
                        lbltotalcount.style.display = "none";
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

					function validate(key) {
						//getting key code of pressed key
						var keycode = (key.which) ? key.which : key.keyCode;
						var phn = document.getElementById('txtMobileNo');
						//comparing pressed keycodes
						if ((keycode < 48 || keycode > 57)) {
							return false;
						}
						else {
							//Condition to check textbox contains ten numbers or not
							if (phn.value.length == 10) {
								return true;
							}
							else {
								alert("NOt valis phone no");
								return false;
							}
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
			</div>
		</ContentTemplate>
	</asp:UpdatePanel>
</asp:Content>