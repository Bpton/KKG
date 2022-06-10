<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmTransporterBillV2_FAC.aspx.cs" Inherits="VIEW_frmTransporterBillV2_FAC" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../css/bootstrap-multiselect.css" rel="stylesheet" />
    <script type="text/javascript" src="../js/bootstrap.js"></script>
    <script type="text/javascript" src="../js/bootstrap-multiselect.js"></script>
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

         <Triggers>                
            <asp:PostBackTrigger ControlID="btnExport" />
         </Triggers> 
        <ContentTemplate>
            <%--<div class="page_title">
                <span class="title_icon"><span class="computer_imac"></span></span>
                <h3>
                    Transporter Bill
                </h3>
            </div>--%>
            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active" id="trBtn" runat="server">
                                <span class="h_icon_he list_images"></span>
                                <h6>
                                    Transporter Bill</h6>
                                <div class="btn_30_light" style="float: right;" id="btnaddhide" runat="server">
                                    <span id="icon" class="icon add_co"></span>
                                    <asp:Button ID="btnAddMenu" runat="server" Text="Add New Record" CssClass="btn_link"
                                        CausesValidation="false" OnClick="btnAddMenu_Click" /></div>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input" style="padding-left: 10px">
                                                <fieldset>
                                                    <legend>Transporter Details</legend>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr id="tr_billno" runat="server">
                                                    <td class="field_title" width="90">
                                                    <asp:Label ID="Label15" runat="server" Text="Bill Entry No."></asp:Label>
                                                    </td>
                                                    <td class="field_input" width="100" colspan="7">
                                                    <asp:TextBox ID="txtbillentryno" runat="server" Enabled="false" Width="250"></asp:TextBox>
                                                    </td>
                                                    
                                                    </tr>
                                                        <tr>
                                                            <td class="field_title" width="90">
                                                                <asp:Label ID="lblbillDate" runat="server" Text="Entry Date"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td class="field_input" width="70">
                                                                <asp:TextBox ID="txtbillDate" runat="server" Enabled="false" MaxLength="10" placeholder="dd/MM/yyyy"
                                                                    ValidationGroup="SAVE" Width="60"></asp:TextBox>
                                                                <asp:ImageButton ID="imgbtnToCalendar" runat="server" Height="24px" ImageAlign="AbsMiddle"
                                                                    ImageUrl="~/images/calendar.png" Width="24px" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtenderDate" runat="server" BehaviorID="CalendarExtenderDate"
                                                                    CssClass="cal_Theme1" Enabled="true" Format="dd/MM/yyyy" PopupButtonID="imgbtnToCalendar"
                                                                    TargetControlID="txtbillDate" />
                                                            </td>
                                                            <td class="field_title" width="50">
                                                            <asp:Label ID="Label1" runat="server" Text="Depot"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td class="field_input" width="150">
                                                                <asp:DropDownList ID="ddldepot" ValidationGroup="ADD" runat="server" AppendDataBoundItems="True"
                                                                    OnSelectedIndexChanged="ddldepot_SelectedIndexChanged" AutoPostBack="true" class="chosen-select"
                                                                    data-placeholder="Choose Depot" Width="200">
                                                                </asp:DropDownList>
                                                            </td>
                                                            
                                                             <td class="field_title" width="50" id="tdlblexportdepot" runat="server" >
                                                            <asp:Label ID="Label24" runat="server" Text=" Sending Depot"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td class="field_input" width="150" id="tdddlexportdepot" runat="server">
                                                                <asp:DropDownList ID="ddlexportdepot"  runat="server" AppendDataBoundItems="True"
                                                                     class="chosen-select"
                                                                    data-placeholder="Choose Depot" Width="150">
                                                                </asp:DropDownList>
                                                            </td>

                                                            <td class="field_title" width="100">
                                                                <asp:Label ID="Label11" runat="server" Text="Billing from"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td class="field_input" width="150">
                                                                <asp:DropDownList ID="ddlfromstate" ValidationGroup="ADD" runat="server" AppendDataBoundItems="True"
                                                                    class="chosen-select" data-placeholder="Select State" Width="150"
                                                                     OnSelectedIndexChanged="ddlfromstate_SelectedIndexChanged" AutoPostBack="true">
                                                                </asp:DropDownList>
                                                            </td>
                                                            
                                                            <%--<td class="field_title">&nbsp;</td>--%>
                                                        </tr>
                                                        <tr>
                                                        <td class="field_title" width="90">
                                                                <asp:Label ID="lblrdb" runat="server" Text="Billing For"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td class="field_input" width="400">
                                                                <asp:RadioButtonList ID="rdbtype" runat="server" AutoPostBack="true" ForeColor="blue" BackColor="AliceBlue"
                                                                    OnSelectedIndexChanged="rdbtype_SelectedIndexChanged" RepeatDirection="Horizontal" >
                                                                    <asp:ListItem Text="Sale Invoice(Outward)" Value="STKINV" style="color:#01773c; font-weight:bold;"/>
                                                                    <asp:ListItem Text="Stock Transfer" Value="STKTRN" style="color: #ec00a2;"/>
                                                                    <asp:ListItem Text="Depot Received" Value="DPTRCVD" style="color: #ec5a00;"/>
                                                                    <asp:ListItem Text="TPU Despatch(Inward)" Value="STKDES" />
                                                                   <%--<asp:ListItem Text="Others" Value="Others" style="display:none;" />--%> 
                                                                </asp:RadioButtonList>
                                                                
                                                            </td>
                                                             <td id="Td1" runat="server" width="60" class="field_title" style="padding-bottom: 24px;">
                                                                INVOICE<span class="req">*</span>
                                                            </td>
                                                            <td id="tdddlinvoice" runat="server" class="field_input" width="200" style="padding-bottom: 24px;">
                                                                <asp:DropDownList ID="ddlinvoice" ValidationGroup="ADD" runat="server" AppendDataBoundItems="True"
                                                                    class="chosen-select" data-placeholder="Choose Invoice" Width="200" OnSelectedIndexChanged="ddlinvoice_SelectedIndexChanged"
                                                                    AutoPostBack="true">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfv_ddlinvoice" runat="server" ControlToValidate="ddlinvoice" ValidationGroup="ADD"
                                                                ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!" 
                                                                ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>

                                                            </td>
                                                             <td class="field_title" width="90">
                                                                <asp:Label ID="Label12" runat="server" Text="Transporter"></asp:Label>
                                                                <span class="req">*</span>
                                                           </td>
                                                            <td class="field_input" width="150">
                                                                <asp:DropDownList ID="ddltransporter" ValidationGroup="ADD" runat="server" AppendDataBoundItems="True"
                                                                    class="chosen-select" data-placeholder="Choose Transporter" Width="250" OnSelectedIndexChanged="ddltransporter_OnSelectedIndexChanged" AutoPostBack="true">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddltransporter"
                                                                    ValidationGroup="ADD" ErrorMessage="*" ForeColor="Red" InitialValue="0" SetFocusOnError="true"
                                                                    ValidateEmptyText="false"> </asp:RequiredFieldValidator>
                                                            </td>
                                                             <td class="field_title" style="display:none;">
                                                                <asp:TextBox ID="txtGNGNO" runat="server" Width="100"></asp:TextBox>
                                                                <span class="label_intro">GNG No</span>
                                                            </td>
                                                            <td id="tdmodule" runat="server" style="display:none;">
                                                            <table>
                                                            <tr>
                                                                <td class="field_input"> 
                                                                <asp:DropDownList ID="ddlmodule"  runat="server" AppendDataBoundItems="True"
                                                                    class="chosen-select"
                                                                    data-placeholder="Choose Depot " Width="100" >
                                                                    <asp:ListItem Text="Sale Invoice" Value="STKINV"  />
                                                                    <asp:ListItem Text="Stock Transfer" Value="STKTRN"  />
                                                                    <asp:ListItem Text="TPU Despatch" Value="STKDES" />
                                                                   
                                                                </asp:DropDownList>
                                                            </td>
                                                            
                                                            <td class="field_title">
                                                                <asp:Label ID="Label14" runat="server" Text="Billing to"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:DropDownList ID="ddltostate" ValidationGroup="ADD" runat="server" AppendDataBoundItems="True"
                                                                    class="chosen-select" data-placeholder="Select State" Width="150">
                                                                </asp:DropDownList>
                                                            </td>
                                                           
                                                            </tr>
                                                            </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <div id="tdbilldetails" runat="server">
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                         
                                                            <td id="tdlbllr" runat="server" width="80" class="field_title" style="padding-bottom: 24px; padding-left:10px;">
                                                                LR/GR No. <span class="req">*</span>
                                                            </td>
                                                           
                                                            <%--<td id="tdddllrno" runat="server" class="field_input" width="130" style="padding-bottom: 24px;">
                                                                <asp:DropDownList ID="ddlLRNo" ValidationGroup="ADD" runat="server" AppendDataBoundItems="True"
                                                                    class="chosen-select" data-placeholder="Choose LRNO" Width="130" OnSelectedIndexChanged="ddlLRNo_SelectedIndexChanged"
                                                                    AutoPostBack="true">
                                                                </asp:DropDownList>
                                                            </td>--%>
                                                            
                                                            <td id="tdtxtlrno" runat="server" width="125" class="field_input" style="padding-bottom: 24px;">
                                                                <asp:TextBox ID="txtlrno" runat="server" Width="125px"></asp:TextBox>
                                                            </td>
                                                            <td class="field_input" width="100">
                                                                <asp:TextBox ID="txtlrdate" runat="server" Enabled="false" MaxLength="10" placeholder="dd/MM/yyyy"
                                                                    Width="60"></asp:TextBox>
                                                                <asp:ImageButton ID="Imagetxtlrdate" runat="server" Height="24px" ImageAlign="AbsMiddle"
                                                                    ImageUrl="~/images/calendar.png" Width="24px" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="cal_Theme1"
                                                                    Enabled="true" Format="dd/MM/yyyy" PopupButtonID="Imagetxtlrdate" TargetControlID="txtlrdate" />
                                                                <span class="label_intro">LRGR Date</span>
                                                            </td>
                                                            
                                                            <td class="field_input" width="100">
                                                                <asp:TextBox ID="txtbilldate2" runat="server" Enabled="false" MaxLength="10" placeholder="dd/MM/yyyy"
                                                                    Width="60"></asp:TextBox>
                                                                <asp:ImageButton ID="imgbtnToCalendar1" runat="server" Height="24px" ImageAlign="AbsMiddle"
                                                                    ImageUrl="~/images/calendar.png" Width="24px" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                                                                    Enabled="true" Format="dd/MM/yyyy" PopupButtonID="imgbtnToCalendar1" TargetControlID="txtbilldate2" />
                                                                <span class="label_intro">Bill Date</span>
                                                            </td>
                                                            <td class="field_input" width="110">
                                                                <asp:TextBox ID="Txtbillno" runat="server" Width="100" ValidationGroup="ADD" onchange="CHKInvoiceNo();" Enabled="true" onkeypress="return isNumberKeyWithSlashHyphenAtZ(event);"
                                                                    ></asp:TextBox>
                                                                <span class="label_intro">Bill No</span>
                                                                <asp:RequiredFieldValidator ID="CV_Billno" runat="server" ErrorMessage="Bill No. is required!"
                                                                    ControlToValidate="Txtbillno" ValidationGroup="ADD" ValidateEmptyText="false"
                                                                    Display="None" SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                                                    TargetControlID="CV_Billno" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="../images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                            </td>
                                                            <td class="field_input" width="80">
                                                                <asp:TextBox ID="txtBillvalue" runat="server" ValidationGroup="ADD" Width="70" onkeypress="return isNumberKeyWithDot(event);"
                                                                    MaxLength="10" OnTextChanged="txtBillvalue_TextChanged"
                                                                    AutoPostBack="true"></asp:TextBox>
                                                                <span class="label_intro">Bill Value</span>
                                                                <asp:RequiredFieldValidator ID="CV_Billvalue" runat="server" ErrorMessage="Bill Value is required!"
                                                                    ControlToValidate="txtBillvalue" ValidationGroup="ADD" ValidateEmptyText="false"
                                                                    Display="None" SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                                                    TargetControlID="CV_Billvalue" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="../images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                            </td>
                                                            <td class="field_input" width="80">
                                                                <asp:TextBox ID="txttdsdeductablevalue" runat="server" ValidationGroup="ADD" Width="70"
                                                                    onkeypress="return isNumberKeyWithDot(event);" MaxLength="10"
                                                                    OnTextChanged="txttdsdeductablevalue_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                <span class="label_intro">TDS Deductable</span>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="TDS Deductable Value is required!"
                                                                    ControlToValidate="txttdsdeductablevalue" ValidationGroup="ADD" ValidateEmptyText="false"
                                                                    Display="None" SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server"
                                                                    TargetControlID="RequiredFieldValidator2" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="../images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                            </td>
                                                           
                                                            <td class="field_title" id="td_chktdslabel" runat="server" width="100" >
                                                                <asp:Label ID="Label22" Text="TDS Applicable" runat="server" ForeColor="#0066FF" Visible ="false"></asp:Label>
                                                                <asp:Label ID="Label10" Text="GST Applicable" runat="server" ForeColor="#3366FF" ></asp:Label>

                                                            </td>
                                                            <td class="field_input" id="td_chktdsinput" runat="server" width="40">
                                                                <asp:CheckBox ID="chktdsapplicable" runat="server" Text=" " Visible ="false" Enabled ="false"/>
                                                                <asp:CheckBox ID="chkcstapplicable" runat="server" Text=" "  OnCheckedChanged="chkcstapplicable_OnCheckedChanged"
                                                                 AutoPostBack="true" Enabled="false"/>                                                               
                                                            </td>
                                                           <td class="field_title" id="td_chkcstlabel" runat="server">
                                                               
                                                            </td>
                                                            <td class="field_input" colspan="4" id="td_chkcstinput" runat="server">
                                                                
                                                            </td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        </table>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                         <td width="80" class="field_title" style="padding-bottom: 15px; display:none">
                                                                <asp:Label ID="Label2" Text="TDS" runat="server" Visible="False"></asp:Label>
                                                                <asp:Label ID="lbltds" runat="server" Text="(Not Applicable)" ForeColor="Red" Visible="False"
                                                                    Font-Size="XX-Small"></asp:Label><span id="tdsspan" runat="server" class="req">*</span>
                                                            </td>
                                                            <td width="10" class="field_input" style="display:none">
                                                                <asp:TextBox ID="txttdspercentage" runat="server" ValidationGroup="ADD" Enabled="false" 
                                                                    MaxLength="10" onkeypress="return isNumberKeyWithDot(event);" Visible="False"></asp:TextBox>
                                                               <%--<span class="label_intro">Percentage</span>--%> 
                                                            </td>
                                                            <td class="field_input" width="60" style="display:none">
                                                                <asp:TextBox ID="TxtTds" runat="server" ValidationGroup="ADD" Enabled="false"
                                                                    MaxLength="10" onChange="calculation()" onkeypress="return isNumberKeyWithDot(event);" Visible="False"></asp:TextBox>
                                                                <%--<span class="label_intro">Value</span>--%>
                                                            </td>
                                                            <td width="30" class="field_title" style="padding-bottom: 15px; ">
                                                                <asp:Label ID="Label3" Text="CGST" runat="server"></asp:Label><span id="Span2"
                                                                    class="req">&nbsp;</span>
                                                            </td>
                                                            <td class="field_input" width="10" >
                                                                <asp:TextBox ID="txtcgstpercentage" runat="server" ValidationGroup="ADD" AutoPostBack="true"
                                                                    Enabled="false" MaxLength="10" onkeypress="return isNumberKeyWithDot(event);"></asp:TextBox>
                                                                <span class="label_intro">Percentage</span>
                                                            </td>
                                                            <td class="field_input" width="60">
                                                                <asp:TextBox ID="txtcgstvalue" runat="server" ValidationGroup="ADD" Enabled="false"
                                                                    MaxLength="10" onChange="calculation()" onkeypress="return isNumberKeyWithDot(event);"></asp:TextBox>
   
                                                                <span class="label_intro">Value</span>
                                                            </td>

                                                           <td width="30" class="field_title" style="padding-bottom: 15px;">
                                                                <asp:Label ID="Label16" Text="SGST" runat="server"></asp:Label><span id="Span3"
                                                                    class="req">&nbsp;</span>
                                                                    </td>

                                                            
                                                             <td class="field_input" width="40">
                                                                <asp:TextBox ID="txtsgstpercentage" runat="server" ValidationGroup="ADD" AutoPostBack="true"
                                                                    Enabled="false" MaxLength="10" onkeypress="return isNumberKeyWithDot(event);"></asp:TextBox>
                                                                
                                                                <span class="label_intro">Percentage</span>
                                                            </td>

                                                            <td class="field_input" width="60">
                                                                <asp:TextBox ID="txtsgstvalue" runat="server" ValidationGroup="ADD" Enabled="false"
                                                                    MaxLength="10" onChange="calculation()" onkeypress="return isNumberKeyWithDot(event);"></asp:TextBox>
                                                                
                                                                <span class="label_intro">Value</span>
                                                            </td>

                                                             <td width="30" class="field_title" style="padding-bottom: 15px;">
                                                                <asp:Label ID="Label18" Text="IGST" runat="server"></asp:Label><span id="Span4"
                                                                    class="req">&nbsp;</span>
                                                                    </td>
                                                              
                                                              <td class="field_input" width="40">
                                                                <asp:TextBox ID="txtigstpercentage" runat="server" ValidationGroup="ADD" AutoPostBack="true"
                                                                    Enabled="false" MaxLength="10" onkeypress="return isNumberKeyWithDot(event);"></asp:TextBox>
                                                                
                                                                <span class="label_intro">Percentage</span>
                                                            </td>

                                                            <td class="field_input" width="60">
                                                                <asp:TextBox ID="txtigstvalue" runat="server" ValidationGroup="ADD" Enabled="false"
                                                                    MaxLength="10" onChange="calculation()" onkeypress="return isNumberKeyWithDot(event);"></asp:TextBox>
                                                                
                                                                <span class="label_intro">Value</span>
                                                            </td>



                                                             <td width="30" class="field_title" style="padding-bottom: 15px;display:none"" >
                                                                <asp:Label ID="Label21" Text="UGST" runat="server"></asp:Label><span id="Span5"
                                                                    class="req">&nbsp;</span>
                                                                    </td>
                                                              
                                                              <td class="field_input" width="40" style="display:none">
                                                                <asp:TextBox ID="txtugstpercentage" runat="server" ValidationGroup="ADD" AutoPostBack="true"
                                                                    Enabled="false" MaxLength="10" onkeypress="return isNumberKeyWithDot(event);" Text="0"></asp:TextBox>
                                                                
                                                                <span class="label_intro">Percentage</span>
                                                            </td>

                                                            <td class="field_input" width="60" style="display:none">
                                                                <asp:TextBox ID="txtugstvalue" runat="server" ValidationGroup="ADD" Enabled="false"
                                                                    MaxLength="10" onChange="calculation()" onkeypress="return isNumberKeyWithDot(event);" Text="0"></asp:TextBox>
                                                                
                                                                <span class="label_intro">Value</span>
                                                            </td>
                                                            <td class="field_title" width="90" style="padding-bottom: 15px;">
                                                                <asp:Label ID="Label7" Text="Gross Wt.(KG)" runat="server"></asp:Label><span class="req">*</span>
                                                            </td>
                                                            <td class="field_input" width="60" style="padding-bottom: 20px;">
                                                                <asp:TextBox ID="txtgrossweight" runat="server" ValidationGroup="ADD"
                                                                    MaxLength="10" onkeypress="return isNumberKeyWithDot(event);"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfv_txtgrossweight" runat="server" ErrorMessage="Gross weight is required!"
                                                                    ControlToValidate="txtgrossweight" ValidationGroup="ADD" ValidateEmptyText="false"
                                                                    Display="None" SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server"
                                                                    TargetControlID="rfv_txtgrossweight" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="../images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                            </td>
                                                            <td class="field_title" width="50" style="padding-bottom: 15px;">
                                                                <asp:Label ID="Label4" Text="(BASIC + GST) AMT." runat="server"></asp:Label><span class="req">*</span>
                                                            </td>
                                                            <td class="field_input" width="70" style="padding-bottom: 20px;">
                                                                <asp:TextBox ID="Txtamount" runat="server" ValidationGroup="ADD" Enabled="false"
                                                                    MaxLength="10" onkeypress="return isNumberKeyWithDot(event);"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Amount is required!"
                                                                    ControlToValidate="Txtamount" ValidationGroup="ADD" ValidateEmptyText="false"
                                                                    Display="None" SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server"
                                                                    TargetControlID="RequiredFieldValidator5" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="../images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                            </td>
                                                            <td class="field_input" width="70" style="padding-bottom: 20px;">
                                                                <div class="btn_24_blue" id="divbinadd" runat="server">
                                                                    <span class="icon add_co"></span>
                                                                    <asp:Button ID="btnADDGrid" runat="server" CssClass="btn_link" OnClick="btnADDGrid_Click"
                                                                        Text="Add" ValidationGroup="ADD" />
                                                                    <asp:HiddenField ID="hdn_tdsis" runat="server" />
                                                                   
                                                                </div>
                                                            </td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                    </table>
                                                    </div>
                                                </fieldset>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="gridcontent-inner">
                                        <fieldset>
                                            <legend>Transporter Billing Details Added</legend>
                                            <cc1:Grid ID="gvtransporterAdd" runat="server" Serialize="true" AutoGenerateColumns="false"
                                                FolderStyle="../GridStyles/premiere_blue" EnableRecordHover="true" AllowSorting="false"
                                                ShowColumnsFooter="false" AllowPageSizeSelection="false" AllowPaging="false"
                                                PageSize="500" AllowAddingRecords="false">
                                                <FilteringSettings InitialState="Visible" />
                                                <Columns>
                                                    <cc1:Column ID="Column5" HeaderText="Sl.No." AllowEdit="false" AllowDelete="false"
                                                        Width="70px">
                                                        <TemplateSettings TemplateId="tplNumberingSlnoProduct" />
                                                    </cc1:Column>
                                                      <cc1:Column  DataField="GUID" HeaderText="GUID" runat="server" Visible="false" />
                                                    <cc1:Column ID="Column12" DataField="BILLDATE" runat="server" HeaderText="BILL DATE"
                                                        Width="100" Wrap="true">
                                                    </cc1:Column>
                                                     <cc1:Column ID="Column24" DataField="TRANSPORTERID" runat="server" HeaderText="TRANSPORTERID" Visible="false"
                                                        Wrap="true">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column25" DataField="TRANSPORTERNAME" runat="server" HeaderText="TRANSPORTER NAME" Width="150" Wrap="true">
                                                    </cc1:Column>
                                                    <cc1:Column DataField="LRGRNO" runat="server" HeaderText="LR/GR No." Width="100"
                                                        Wrap="true">
                                                    </cc1:Column>
                                                    <cc1:Column DataField="INVNO" runat="server" HeaderText="TYPE NO" Width="150" Wrap="true">
                                                    </cc1:Column>
                                                    <cc1:Column DataField="BILLNO" runat="server" HeaderText="BILL NO" Width="100" Wrap="true">
                                                    </cc1:Column>
                                                    <cc1:Column DataField="BILLAMOUNT" runat="server" HeaderText="BILL AMOUNT" Width="100"
                                                        Wrap="true">
                                                    </cc1:Column>
                                                    <cc1:Column DataField="TDSPERCENTRAGE" runat="server" HeaderText="TDS %" Width="80"
                                                        Wrap="true" Visible="false">
                                                    </cc1:Column>
                                                    <cc1:Column DataField="TDS" runat="server" HeaderText="TDS VALUE" Width="100" Wrap="true" Visible="false">
                                                    </cc1:Column>
                                                     <cc1:Column  DataField="CGSTID" runat="server" HeaderText="CGSTID"
                                                        Width="110" Wrap="true" Visible="false" >
                                                    </cc1:Column>

                                                    <cc1:Column DataField="CGSTPERCENTAGE" runat="server" HeaderText="CGST %"
                                                        Width="110" Wrap="true">
                                                    </cc1:Column>

                                                     <cc1:Column  DataField="CGSTAX" runat="server" HeaderText="CGST VALUE"
                                                        Width="110" Wrap="true">
                                                    </cc1:Column>

                                                     <cc1:Column ID="Column13"  DataField="SGSTID" runat="server" HeaderText="SGSTID"
                                                        Width="110" Wrap="true" Visible="false">
                                                    </cc1:Column>

                                                    <cc1:Column ID="Column14" DataField="SGSTPERCENTAGE" runat="server" HeaderText="SGST %"
                                                        Width="110" Wrap="true">
                                                    </cc1:Column>

                                                     <cc1:Column ID="Column15"  DataField="SGSTAX" runat="server" HeaderText="SGST VALUE"
                                                        Width="110" Wrap="true">
                                                    </cc1:Column>

                                                     <cc1:Column ID="Column16"  DataField="IGSTID" runat="server" HeaderText="IGSTID"
                                                        Width="110" Wrap="true" Visible="false">
                                                    </cc1:Column>

                                                    <cc1:Column ID="Column17" DataField="IGSTPERCENTAGE" runat="server" HeaderText="IGST %"
                                                        Width="110" Wrap="true">
                                                    </cc1:Column>

                                                     <cc1:Column ID="Column18"  DataField="IGSTAX" runat="server" HeaderText="IGST VALUE"
                                                        Width="110" Wrap="true">
                                                    </cc1:Column>

                                                    <cc1:Column ID="Column19" DataField="UGSTPERCENTAGE" runat="server" HeaderText="UGST %"
                                                        Width="110" Wrap="true" Visible="false">
                                                    </cc1:Column>

                                                     <cc1:Column ID="Column20"  DataField="UGSTAX" runat="server" HeaderText="UGST VALUE"
                                                        Width="110" Wrap="true" Visible="false">
                                                    </cc1:Column>
                                                   
                                                    <cc1:Column DataField="GROSSWEIGHT" runat="server" HeaderText="GROSS WEIGHT " Width="120"
                                                        Wrap="true">
                                                    </cc1:Column>
                                                    <cc1:Column DataField="NETAMOUNT" runat="server" HeaderText=" (BASIC + GST) AMOUNT " Width="100"
                                                        Wrap="true">
                                                    </cc1:Column>

                                                    <cc1:Column ID="Column21" DataField="BILLINGTOSTATEID" runat="server" HeaderText="BILLINGTOSTATEID " Width="100"
                                                        Wrap="true" Visible="false">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column22" DataField="BILLINGTOSTATENAME" runat="server" HeaderText="BILLING TO " Width="100"
                                                        Wrap="true" Visible="false">
                                                    </cc1:Column>
                                                    
                                                    <cc1:Column AllowDelete="true" AllowEdit="false" HeaderText="DELETE" Width="80">
                                                        <TemplateSettings TemplateId="deleteBtnTemplateMap" />
                                                    </cc1:Column>
                                                </Columns>
                                                <Templates>
                                                    <cc1:GridTemplate runat="server" ID="tplNumberingSlnoProduct">
                                                        <Template>
                                                            <asp:Label ID="lblslnoProduct" runat="server" Text='<%#Container.PageRecordIndex+1 %>'></asp:Label>
                                                        </Template>
                                                    </cc1:GridTemplate>
                                                    <cc1:GridTemplate ID="deleteBtnTemplateMap" runat="server">
                                                        <Template>
                                                            <a href="javascript: //" id="btnGridDelete_<%# Container.PageRecordIndex %>" class="action-icons c-delete"
                                                                onclick="CallDeleteServerMethod(this)"></a>
                                                        </Template>
                                                    </cc1:GridTemplate>
                                                </Templates>
                                                <ScrollingSettings ScrollHeight="180" ScrollWidth="100%" />
                                            </cc1:Grid>
                                            <asp:Button ID="btngrddelete" runat="server" CausesValidation="false" OnClick="btngrddelete_Click"
                                                OnClientClick="return confirm('Are you sure you want to delete this record?');"
                                                Style="display: none" Text="grddelete" />
                                            <asp:HiddenField ID="hdndtDelete" runat="server" />
                                        </fieldset>
                                    </div>
                                    
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input" valign="top" style="padding-left: 10px; width: 60%;">
                                             <fieldset>
                                                    <legend>REMARKS & OTHERS Details</legend>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                                        <tr>
                                                            <td width="150" class="field_title" >
                                                                <asp:Label ID="Label5" Text="Remarks" runat="server"></asp:Label>
                                                            </td>
                                                            <td width="15" class="field_title">&nbsp;
                                                            </td>
                                                            <td width="310" class="field_input" >
                                                                <asp:TextBox ID="txtremarks" runat="server" TextMode="MultiLine" Style="width: 290px;
                                                                    height: 30px" MaxLength="255"></asp:TextBox>
                                                            </td>
                                                            <td width="15" class="field_title">&nbsp;
                                                            </td>
                                                            <td width="100" align="left" class="field_title">
                                                                <asp:Label ID="lblCheckerNote" Text="Rejection Note" runat="server"></asp:Label>
                                                            </td>
                                                            <td width="310" class="field_input">
                                                                <asp:TextBox ID="txtCheckerNote" runat="server" TextMode="MultiLine" Enabled="false"
                                                                    Style="width: 200px; height: 30px" MaxLength="255"> </asp:TextBox>
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="120" class="field_title">
                                                                <asp:Label ID="Label13" Text="Transfer To HO" runat="server" ForeColor="#0066FF" Visible="false"></asp:Label><br />
                                                                <asp:Label ID="Label19" Text="Reverse Charge" runat="server" ForeColor="#0066FF"></asp:Label>
                                                            </td>
                                                            <td class="field_input" style="padding-bottom:14px;">
                                                            <asp:CheckBox ID="chktransferHO" runat="server" Enabled="true" Visible="false"/><br />
                                                             <asp:CheckBox ID="chkReversecharge" runat="server" OnCheckedChanged="chkReversecharge_OnCheckedChanged" AutoPostBack="true"  />
                                                             </td>
                                                             
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                            <td class="field_input" valign="top" style="padding-left: 10px; padding-right:30px; width: 40%;">
                                            <fieldset>
                                                    <legend>Amount&nbsp;Details</legend>
                                                    <table border="2" cellspacing="0" cellpadding="0" width="100%">
                                                        <tr>
                                                            <td valign="top" width="60%">
                                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                    <tr>
                                                                    <td class="innerfield_title" style="color: #1e69de;" >
                                                                        <asp:Label ID="Label23" Text="TDS Applicable" runat="server" ForeColor="#0066FF" > </asp:Label>
                                                                   </td>
                                                                    <td >
                                                                        <asp:CheckBox ID="chktdsapplicable1" runat="server" Text=" " OnCheckedChanged="chktdsapplicable1_OnCheckedChanged" AutoPostBack="true"/>
                                                                        
                                                                   </td>
                                                                   </tr>
                                                                   <tr>
                                                                        <td class="innerfield_title" style="color: #1e69de;">
                                                                            <asp:Label ID="Label6" Text="Gross Weight(Kg.)" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtsumgrossweight" runat="server" CssClass="x_large" Enabled="false" Style="text-align: right;
                                                                    font-weight: bold; color: Black;" MaxLength="10" onkeypress="return isNumberKeyWithDot(event);"
                                                                    Text="0.00"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="innerfield_title" style="color: #1e69de;">
                                                                            <asp:Label ID="Label311" Text="Total Bill Amt." runat="server"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtsumbillvalue" runat="server" CssClass="x_large" Enabled="false" Style="text-align: right;
                                                                    font-weight: bold; color: Black;" MaxLength="10" onkeypress="return isNumberKeyWithDot(event);"
                                                                    Text="0.00"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="innerfield_title" style="color: #1e69de;">
                                                                            <asp:Label ID="Label59" Text="TDS Deductable Amt." runat="server"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtsumtdsdeuctablevalue" runat="server" CssClass="x_large" Enabled="false" Style="text-align: right;
                                                                    font-weight: bold; color: Black;" MaxLength="10" onkeypress="return isNumberKeyWithDot(event);"
                                                                    Text="0.00"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="innerfield_title" style="color: #1e69de;">
                                                                            <asp:Label ID="Label8" Text="Total TDS" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox  ID="txtTDSpercentage1" runat="server" Enabled="false" MaxLength="10"
                                                                                            Style="text-align: right; font-weight: bold; color: Black;" 
                                                                                            Text="0.00" Width="32" >
                                                                            </asp:TextBox>&nbsp;
                                                                            <asp:TextBox    ID="txtsumtds" runat="server" Enabled="false" MaxLength="10"
                                                                                            Style="text-align: right; font-weight: bold; color: Black;" onkeypress="return isNumberKeyWithDot(event);"
                                                                                             Width="75">
                                                                            </asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td valign="top" style="width: 50%; padding-right: 20px">
                                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                    <tr>
                                                                        <td class="innerfield_title" width="40%" style="color: #1e69de;">
                                                                            <asp:Label ID="lblcgst" Text="CGST AMT." runat="server"></asp:Label>
                                                                        </td>
                                                                        <td style="float: right;">
                                                                        <asp:TextBox ID="txtsumcgst" runat="server" Enabled="false" MaxLength="10"
                                                                    Style="text-align: right; font-weight: bold; color: Black;" onkeypress="return isNumberKeyWithDot(event);"
                                                                    Text="0.00"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="innerfield_title" style="color: #1e69de;">
                                                                            <asp:Label ID="lblsgst" Text="SGST AMT." runat="server"></asp:Label>
                                                                        </td>
                                                                        <td style="float: right;">
                                                                        <asp:TextBox ID="txtsumsgst" runat="server" Enabled="false" MaxLength="10"
                                                                    Style="text-align: right; font-weight: bold; color: Black;" onkeypress="return isNumberKeyWithDot(event);"
                                                                    Text="0.00"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="innerfield_title" style="color: #1e69de;">
                                                                            <asp:Label ID="lbligst" Text="IGST AMT." runat="server"></asp:Label>
                                                                        </td>
                                                                        <td style="float: right;">
                                                                            <asp:TextBox ID="txtsumigst" runat="server" Enabled="false" MaxLength="10"
                                                                    Style="text-align: right; font-weight: bold; color: Black;" onkeypress="return isNumberKeyWithDot(event);"
                                                                    Text="0.00"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="innerfield_title" style="color: #1e69de;">
                                                                            <asp:Label ID="lblugst" Text="UGST AMT." runat="server"></asp:Label>
                                                                        </td>
                                                                        <td style="float: right;">
                                                                <asp:TextBox ID="txtsumugst" runat="server" Enabled="false" MaxLength="10"
                                                                    Style="text-align: right; font-weight: bold; color: Black;" onkeypress="return isNumberKeyWithDot(event);"
                                                                    Text="0.00"></asp:TextBox>
                                                                        </td>
                                                                      </tr>
                                                                     <tr>
                                                                      <td class="innerfield_title" style="color: #1e69de;display:none">
                                                                            <asp:Label ID="Label9" Text="NET AMT." runat="server"></asp:Label>
                                                                        </td>     
                                                                        <td style="float: right;">
                                                                            <asp:TextBox ID="txtsumnetamt" runat="server" Enabled="false" MaxLength="10"
                                                                    Style="text-align: right; font-weight: bold; color: Black;" onkeypress="return isNumberKeyWithDot(event);"
                                                                    Text="0.00" Visible="false"></asp:TextBox>
                                                                        </td>
                                                                      </tr>

                                                                      <tr>
                                                                         <td class="innerfield_title" style="color: #1e69de;">
                                                                            <asp:Label ID="Label25" Text="NET AMT." runat="server"></asp:Label>
                                                                        </td>     
                                                                        <td style="float: right;">
                                                                            <asp:TextBox ID="txtsumnetamtshow" runat="server" Enabled="false" MaxLength="10"
                                                                    Style="text-align: right; font-weight: bold; color: Black;" onkeypress="return isNumberKeyWithDot(event);"
                                                                    Text="0.00"></asp:TextBox>
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

                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td" id="reason" runat="server">
                                        <tr>
                                            <td class="field_input" valign="top" style="padding-left: 10px; width: 60%;">
                                             <fieldset>
                                                    <legend>Reason Details(TDS)</legend>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                                        <tr>
                                                            <td width="100" class="field_title" >
                                                                <asp:Label ID="Label17" Text="Reason" runat="server"></asp:Label>
                                                            </td>                                                         
                                                            <td width="310" class="field_input" >
                                                               <asp:DropDownList ID="ddlreason" Width="250" runat="server" class="chosen-select"
                                                                    data-placeholder="Choose" AppendDataBoundItems="True">
                                                              </asp:DropDownList>
                                                            </td>   
                                                            <td>&nbsp;</td>                                                        
                                                        </tr>                                                        
                                                    </table>
                                                </fieldset>
                                            </td>                                            
                                    </tr>
                                    </table>
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td" id="td_rcmreason" runat="server">
                                        <tr>
                                            <td class="field_input" valign="top" style="padding-left: 10px; width: 60%;">
                                             <fieldset>
                                                    <legend>Reason Details(GST-RCM)</legend>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                                        <tr>
                                                            <td width="100" class="field_title" >
                                                                <asp:Label ID="Label20" Text="Reason(GST RCM)" runat="server"></asp:Label>
                                                            </td>                                                         
                                                            <td width="310" class="field_input" >
                                                               <asp:DropDownList ID="ddlgstreason" Width="250" runat="server" class="chosen-select"
                                                                    data-placeholder="Choose" AppendDataBoundItems="True">
                                                              </asp:DropDownList>
                                                            </td>   
                                                            <td>&nbsp;</td>                                                        
                                                        </tr>                                                        
                                                    </table>
                                                </fieldset>
                                            </td>                                            
                                    </tr>
                                    </table>
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input" valign="top" style="padding-left: 10px; width: 60%;" runat="server" id="tdSaveClose">
                                             <fieldset>
                                                    <legend>Save/Close</legend>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                                        <tr>
                                                           <td class="field_input">
                                                                <div class="btn_24_blue" id="btnsubmitdiv" runat="server">
                                                                    <span class="icon disk_co"></span>
                                                                    <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="btn_link" ValidationGroup="SAVE"
                                                                        OnClick="btnSubmit_Click" />
                                                                </div>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                                <div class="btn_24_blue">
                                                                    <span class="icon cross_octagon_co"></span>
                                                                    <asp:Button ID="btnCancel" runat="server" Text="Close" CssClass="btn_link" CausesValidation="false"
                                                                        OnClick="btnCancel_Click" />
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
                                                                
                                                                <asp:HiddenField runat="server" ID="hdntrnsID" />
                                                                <asp:HiddenField runat="server" ID="hdnMode" />
                                                                <asp:HiddenField runat="server" ID="hdntdspercentage" />
                                                                <asp:HiddenField runat="server" ID="hdncgsttaxpercentage" />
                                                                <asp:HiddenField runat="server" ID="hdnsgsttaxpercentage" />
                                                                <asp:HiddenField runat="server" ID="hdnigsttaxpercentage" />
                                                                <asp:HiddenField runat="server" ID="hdnugsttaxpercentage" />
                                                                <asp:HiddenField runat="server" ID="Hdn_Print" />
                                                                
                                                                
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

                                                         <td width="61" class="field_title">
                                                                Region<span class="req">*</span>
                                                            </td>
                                                            <td width="300" class="field_input">
                                                                <asp:DropDownList ID="ddlregion" runat="server" class="chosen-select" Width="290"
                                                                  >
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlregion"
                                                                     Display="Dynamic" InitialValue="0" ErrorMessage="Select Region"
                                                                    ForeColor="Red" >*</asp:RequiredFieldValidator>
                                                               
                                                            </td>
                                                        <td width="80">
                                                            <label>
                                                                From Date</label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td width="130">
                                                            <asp:TextBox ID="txtFromDate" runat="server" MaxLength="10" Width="70" Enabled="false"
                                                                placeholder="dd/MM/yyyy" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="Search"
                                                                Font-Bold="true"></asp:TextBox>
                                                            <asp:ImageButton ID="imgPopup" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarFromDate" PopupButtonID="imgPopup" runat="server"
                                                                TargetControlID="txtFromDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>
                                                        <td width="70">
                                                            <label>
                                                                To Date</label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td width="130">
                                                            <asp:TextBox ID="txtToDate" runat="server" MaxLength="10" Width="70" Enabled="false"
                                                                placeholder="dd/mm/yyyy" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="Search"
                                                                Font-Bold="true"></asp:TextBox>
                                                            <asp:ImageButton ID="imgPopuptodate" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtenderToDate" PopupButtonID="imgPopuptodate"
                                                                runat="server" TargetControlID="txtToDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>
                                                        <td width="110">
                                                            <div class="btn_24_blue">
                                                                <span class="icon find_co"></span>
                                                                <asp:Button ID="btnSearchtranporterbill" runat="server" Text="Search" CssClass="btn_link"
                                                                    ValidationGroup="Search" OnClick="btnSearchtranporterbill_Click" />
                                                            </div>
                                                        </td>
                                                        <td width="200">
                                                            <div class="btn_24_blue">
                                                            <span class="icon doc_excel_table_co"></span><a href="#" onclick="exportToExcel();" >
                                                        <asp:Button ID="btnExport" runat="server" Text="Export Excel" CssClass="btn_link" OnClientClick="return false;" CausesValidation="false" /></a>
                                                                <%--<asp:Button ID="btngeneraltemp" runat="server"  Text="Export to excel" CssClass="btn_link" OnClick="btngeneraltemp_Click" CausesValidation="false"/>--%> 
                                                           
                                                        </td>
                                                        <td>&nbsp;</td>
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
                                        <cc1:Grid ID="gvtransporterbill" runat="server" CallbackMode="false" Serialize="true" PageSize="500"
                                            FolderStyle="../GridStyles/premiere_blue" AutoGenerateColumns="false" AllowPageSizeSelection="true"
                                            OnDeleteCommand="DeleteRecord" AllowAddingRecords="false" AllowFiltering="true" AllowSorting="false"
                                            OnRowDataBound="gvtransporterbill_RowDataBound">
                                            <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" OnClientDelete="OnDeleteTransporterDetails" />
                                            <ExportingSettings AppendTimeStamp="false" FileName="TransporterBillEntry" ExportDetails="true" ExportTemplates="true"/>
                                            <FilteringSettings InitialState="Visible" />
                                            <Columns>
                                                <cc1:Column ID="Column1" DataField="TRANSPORTERBILLID" HeaderText="TRANSPORTERBILLID"
                                                    runat="server" Visible="false" />
                                                <cc1:Column ID="Column11" HeaderText="SL" AllowEdit="false" AllowDelete="false" Width="40">
                                                    <TemplateSettings TemplateId="tempsl" />
                                                </cc1:Column>
                                                 <cc1:Column ID="Column244" DataField="DEPOTNAME" HeaderText="DEPOT" Width="100"
                                                    runat="server" Wrap="true">
                                                    
                                                </cc1:Column>
                                                <cc1:Column ID="Column2" DataField="TRANSPORTERBILLNO" HeaderText="ENTRY NO." Width="180"
                                                    runat="server" Wrap="true">
                                                </cc1:Column>
                                                 <cc1:Column ID="Column3" DataField="ENTRYDATE" HeaderText="ENTRY DATE" Width="80"
                                                    runat="server" Wrap="true">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="EqualTo" />
                                                        <cc1:FilterOption Type="Contains" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                  <cc1:Column ID="Column29" DataField="BILLNO" HeaderText="BILL NO." Width="100" runat="server" Wrap="true">
                                                </cc1:Column>    

                                                 <cc1:Column ID="Column34" DataField="VOUCHERNO" HeaderText="TRANSPORTER VOUCHERNO" Width="120"
                                                    runat="server" Wrap="true">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="EqualTo" />
                                                        <cc1:FilterOption Type="Contains" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                
                                                 <cc1:Column ID="Column33" DataField="TRANSPORTERBILLDATE" HeaderText="TRANSPORTER BILLDATE" Width="100"
                                                    runat="server" Wrap="true">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="EqualTo" />
                                                        <cc1:FilterOption Type="Contains" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                                                           
                                                <cc1:Column ID="Column4" DataField="TRANSPORTERNAME" HeaderText="TRANSPORTER NAME" Width="240"
                                                    runat="server" Wrap="true">
                                                </cc1:Column>
                                                <cc1:Column ID="Column71" DataField="BILLTYPE" HeaderText="BILL TYPE" Width="120" runat="server" Wrap="true">
                                                </cc1:Column>
                                                <cc1:Column ID="Column7" DataField="TOTALGROSSWEIGHT" HeaderText="WGT(kgs)" Width="80"  HeaderAlign="right" Align="right"
                                                    runat="server" Wrap="true">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="EqualTo" />
                                                        <cc1:FilterOption Type="Contains" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                 <cc1:Column ID="Column26"  DataField="TOTALBILLAMOUNT" HeaderText="GROSS AMT." runat="server"
                                                    Width="100" Wrap="true">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="EqualTo" />
                                                        <cc1:FilterOption Type="Contains" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column8" DataField="TOTALTDS" HeaderText="TDS" runat="server" Width="80"  HeaderAlign="right" Align="right"
                                                    Wrap="true" >
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="EqualTo" />
                                                        <cc1:FilterOption Type="Contains" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column9" DataField="TOTALSERVICETAX" HeaderText="GST(INPUT)" Width="90"  HeaderAlign="right" Align="right"
                                                    runat="server" Wrap="true"  >
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="EqualTo" />
                                                        <cc1:FilterOption Type="Contains" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column10" DataField="TOTALNETAMOUNT" HeaderText="NET AMOUNT"  HeaderAlign="right" Align="right" 
                                                    Width="105" runat="server" Wrap="true" Visible="false">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="EqualTo" />
                                                        <cc1:FilterOption Type="Contains" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                 <cc1:Column ID="Column23" DataField="DEDUCTABLETDSAMT" HeaderText="DEDUCTABLE TDS AMT" runat="server"
                                                    Width="150" Wrap="true" Visible="false">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="EqualTo" />
                                                        <cc1:FilterOption Type="Contains" />
                                                    </FilterOptions>
                                                </cc1:Column>

                                                 <cc1:Column ID="Column28" DataField="NETAMT" HeaderText="NET AMT" Width="100"  HeaderAlign="right" Align="right"
                                                    runat="server" Wrap="true"  >
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="EqualTo" />
                                                        <cc1:FilterOption Type="Contains" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                               
                                                 <cc1:Column ID="Column27" DataField="TOTALSERVICETAXRCM" HeaderText="GST(RCM)" Width="100"  HeaderAlign="right" Align="right"
                                                    runat="server" Wrap="true"  >
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="EqualTo" />
                                                        <cc1:FilterOption Type="Contains" />
                                                    </FilterOptions>
                                                </cc1:Column>

                                                <cc1:Column ID="Column6" DataField="ISVERIFIEDDESC" HeaderText="APPROVAL" runat="server"
                                                    Width="100" Wrap="true">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="EqualTo" />
                                                        <cc1:FilterOption Type="Contains" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                 <cc1:Column ID="Column31" DataField="DAYEND" HeaderText="DAYEND STATUS" runat="server"
                                                    Width="150" Wrap="true" Visible="false">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="EqualTo" />
                                                        <cc1:FilterOption Type="Contains" />
                                                    </FilterOptions>
                                                </cc1:Column>

                                                 <cc1:Column ID="Column32" DataField="USERNAME" HeaderText="ENTRY USER" runat="server"
                                                    Width="100" Wrap="true">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="EqualTo" />
                                                        <cc1:FilterOption Type="Contains" />
                                                    </FilterOptions>
                                                </cc1:Column>

                                                 <cc1:Column ID="Column245"  DataField="APPROVALPERSONNAME" HeaderText="APPROVAL PERSON" runat="server"
                                                    Width="140" Wrap="true">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="EqualTo" />
                                                        <cc1:FilterOption Type="Contains" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column30" AllowEdit="false" AllowDelete="false" HeaderText="PRINT"
                                                    runat="server" Width="60" Wrap="true">
                                                    <TemplateSettings TemplateId="PrintBtnTemplate" />
                                                </cc1:Column>
                                                <cc1:Column ID="Column92" AllowEdit="true" AllowDelete="false" HeaderText="EDIT"
                                                    runat="server" Width="60">
                                                    <TemplateSettings TemplateId="editBtnTemplate" />
                                                </cc1:Column>
                                                <cc1:Column ID="Column246"  HeaderText="DELETE" AllowEdit="false" AllowDelete="true"
                                                    Width="70">
                                                    <TemplateSettings TemplateId="deleteBtnTemplate" />
                                                </cc1:Column>
                                            </Columns>
                                            <FilteringSettings MatchingType="AnyFilter" />
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="tempsl">
                                                    <Template>
                                                        <asp:Label ID="lblslnoProduct1" runat="server" Text='<%#Container.PageRecordIndex+1 %>'></asp:Label>
                                                    </Template>
                                                </cc1:GridTemplate>
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
                                                        <a href="javascript: //" class="action-icons c-delete" title="Delete" onclick="gvtransporterbill.delete_record(this)">
                                                        </a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="PrintBtnTemplate">
                                                    <Template>
                                                        <a href="javascript: //" class="filter_btn printer_co" id="btnPrint_<%# Container.PageRecordIndex %>"
                                                            onclick="CallServerMethodPrint(this)"></a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <ScrollingSettings ScrollWidth="100%" ScrollHeight="350" NumberOfFixedColumns="4" />
                                        </cc1:Grid>
                                        <asp:HiddenField ID="hdnTid" runat="server" />
                                        <asp:Button ID="btngrdedit" runat="server" Text="grdedit" OnClick="btngridedit_Click"
                                            Style="display: none" CausesValidation="false" />
                                            <asp:Button ID="btnPrint" runat="server" Text="Print" Style="display: none" OnClick="btnPrint_Click"
                                            CausesValidation="false" />
                                    </div>
                                </asp:Panel>
                                <div>
                                </div>
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

                 function reFocus(id) {
                     document.getElementById(id).blur();
                     document.getElementById(id).focus();
                 }
    </script>

     <script type="text/javascript">
         function calculation() {
             var nettotal = 0;
             var tdstotal = 0;
             var taxtotal = 0;
             var totalcgstamt = 0;
             var totalsgstamt = 0;
             var totaligstamt = 0;
             var totalugstamt = 0;

             var cgstpercentage = 0;
             var cgstvalue = 0;
             var sgstpercentage = 0;
             var sgstvalue = 0;
             var igsrpercentage = 0;
             var isgstvalue = 0;
             var ugsrpercentage = 0;



             var billvalue = Number(document.getElementById("<%=txtBillvalue.ClientID %>").value);
             if (isNaN(billvalue)) {
                 billvalue = 0;
             }
             document.getElementById("<%=txttdsdeductablevalue.ClientID %>").value = billvalue.toFixed(2);


             var TDSpercentage = Number(document.getElementById("<%=hdntdspercentage.ClientID %>").value);
             tdstotal = (billvalue * TDSpercentage) / 100;
             if (isNaN(tdstotal)) {
                 tdstotal = 0;      //tds value
             }

             cgstpercentage = Number(document.getElementById("<%=txtcgstpercentage.ClientID %>").value);
             totalcgstamt = (billvalue * cgstpercentage) / 100;
             if (isNaN(totalcgstamt)) {
                 totalcgstamt = 0;      //CGST
             }

             sgstpercentage = Number(document.getElementById("<%=txtsgstpercentage.ClientID %>").value);
             totalsgstamt = (billvalue * sgstpercentage) / 100;
             if (isNaN(totalsgstamt)) {
                 totalsgstamt = 0;      //SGST
             }

             igsrpercentage = Number(document.getElementById("<%=txtigstpercentage.ClientID %>").value);
             totaligstamt = (billvalue * igsrpercentage) / 100;
             if (isNaN(totaligstamt)) {
                 totaligstamt = 0;      //IGST
             }

             ugsrpercentage = Number(document.getElementById("<%=txtugstpercentage.ClientID %>").value);
             totaligstamt = (billvalue * ugsrpercentage) / 100;
             if (isNaN(totalugstamt)) {
                 totalugstamt = 0;      //IGST
             }

             nettotal = billvalue - tdstotal + totalcgstamt + totalsgstamt + totaligstamt;
             document.getElementById("<%=TxtTds.ClientID %>").value = tdstotal.toFixed(2);
             document.getElementById("<%=txtcgstvalue.ClientID %>").value = totalcgstamt.toFixed(2);
             document.getElementById("<%=txtsgstvalue.ClientID %>").value = totalsgstamt.toFixed(2);
             document.getElementById("<%=txtigstvalue.ClientID %>").value = totaligstamt.toFixed(2);
             document.getElementById("<%=txtugstvalue.ClientID %>").value = totalugstamt.toFixed(2);
             document.getElementById("<%=Txtamount.ClientID %>").value = nettotal.toFixed(2);
         }
    </script>
   
            <script type="text/javascript">
                var searchTimeout = null;
                function FilterTextBox_KeyUp() {
                    searchTimeout = window.setTimeout(performSearch, 500);
                }
                function performSearch() {
                    var searchValue = document.getElementById('FilterTextBox').value;
                    //         var searchValue = FilterTextBox.value();
                    if (searchValue == FilterTextBox.WatermarkText) {
                        searchValue = '';
                    }
                    gvMarginSetUp.addFilterCriteria('TYPENAME', OboutGridFilterCriteria.Contains, searchValue);
                    gvMarginSetUp.addFilterCriteria('BSNAME', OboutGridFilterCriteria.Contains, searchValue);
                    gvMarginSetUp.executeFilter();
                    searchTimeout = null;

                    return false;
                }
            </script>
            <script type="text/javascript">
                function OnBeforeDelete(record) {
                    record.Error = '';
                    document.getElementById("<%=hdntrnsID.ClientID %>").value = record.TRANSPORTERBILLID;
                    if (confirm("Are you sure you want to delete? "))
                        return true;
                    else
                        return false;
                }
                function OnDeleteTransporterDetails(record) {
                    alert(record.Error);
                }
            </script>
            <script type="text/javascript">

                function CallServerMethod(oLink) {
                    //                        document.getElementById("<%=hdnTid.ClientID %>").value = '';
                    var iRecordIndex = oLink.id.toString().replace("btnGridEdit_", "");
                    document.getElementById("<%=hdntrnsID.ClientID %>").value = gvtransporterbill.Rows[iRecordIndex].Cells[0].Value;
                    document.getElementById("<%=btngrdedit.ClientID %>").click();

                }

                function CallServerMethodPrint(oLink) {
                    var iRecordIndex = oLink.id.toString().replace("btnPrint_", "");
                    document.getElementById("<%=hdntrnsID.ClientID %>").value = gvtransporterbill.Rows[iRecordIndex].Cells[0].Value;
                    document.getElementById("<%=Hdn_Print.ClientID %>").value = gvtransporterbill.Rows[iRecordIndex].Cells[4].Value;
                    document.getElementById("<%=btnPrint.ClientID %>").click();
                }

                function attachFlyoutToLink(oLink, Action) {
                    var ele = document.getElementById("<%=pnlDisplay.ClientID%>");
                    if (ele.style.display == "") {
                        ele.style.display = "none";
                        document.getElementById("<%=pnlAdd.ClientID%>").style.display = "";
                    }
                    clearFlyout();
                    populateEditControls(oLink.id.toString().replace("btnGridEdit_", ""));
                }



                function CallDeleteServerMethod(oLink) {
                    var iRecordIndexdelete = oLink.id.toString().replace("btnGridDelete_", "");
                    document.getElementById("<%=hdndtDelete.ClientID %>").value = gvtransporterAdd.Rows[iRecordIndexdelete].Cells[1].Value;
                    document.getElementById("<%=btngrddelete.ClientID %>").click();
                }
                function OnDeleteTransporterDetails(record) {
                    alert(record.Error);
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
                function isNumberKeyWithSlashHyphenAtZ(evt) {
                    var charCode = (evt.which) ? evt.which : event.keyCode;
                    if ((charCode < 47 || charCode > 57) && (charCode != 45) && (charCode < 65 || charCode > 90) && (charCode < 97 || charCode > 122))
                        return false;

                    return true;
                }
            </script>

            <script type="text/javascript">
        function CHKInvoiceNo() {
            var txtInvoiceNo1 = "ContentPlaceHolder1_Txtbillno";            
            var txtInvoiceNo = document.getElementById(txtInvoiceNo1).value;
            var txtInvoiceNoL = txtInvoiceNo.length;  
          for (var i = 0; i < txtInvoiceNoL; i++) {  
            
               var n1 = txtInvoiceNo.charCodeAt(i);                                   
              if ((n1 >= 47 && n1 <= 57) || (n1 >= 65 && n1 <= 90) || (n1 >= 97 && n1 <= 122) || n1==45) {

               }
               else {
                    alert('Invalid Bill No.');
                  document.getElementById(txtInvoiceNo1).value = "";
                   return false;
               }
            }
            var j = 0;
            for (var i = 0; i < txtInvoiceNoL; i++) {  
            
               var n1 = txtInvoiceNo.charCodeAt(i);                                   
                if (n1 != 48) {
                    j = 1;
                }              
            }
            if (j == 0)
            {
                alert('Invalid Bill No.');
                document.getElementById(txtInvoiceNo1).value = "";
                return false;
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
             <script type="text/javascript">
                 function exportToExcel() {
                     gvtransporterbill.exportToExcel();
                 }         
    </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>