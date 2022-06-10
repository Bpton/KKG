<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmMMPPQualityControl.aspx.cs" Inherits="VIEW_frmMMPPQualityControl" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls"
    TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .MyDDL
        {
            width: 10px;
            direction: rtl;
            text-align: left;
        }
    </style>
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
        function disableautocompletion(id) {
            var TextBoxControl = document.getElementById(id);
            TextBoxControl.setAttribute("autocomplete", "off");
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
            
            <div id="content">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active">
                                <span class="h_icon_he list_images"></span>
                                <h6>
                                    Quality Assurance Details</h6>
                                <div class="btn_30_light" style="float: right;" id="divnewentry" runat="server">
                                    <span id="icon" class="icon add_co"></span>
                                    <asp:Button ID="btnnewentry" runat="server" Text="Add New Record" CssClass="btn_link"
                                        OnClick="btnnewentry_Click" CausesValidation="false" />
                                </div>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="InputTable" runat="server">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input" style="padding-left: 10px;">
                                                <fieldset>
                                                    <legend>QUALITY ASSURANCE ENTRY DATE</legend>
                                                    <table width="100%" cellpadding="0" cellspacing="0" " border="0">
                                                     
                                                     <tr runat="server" id="AutoQCNo">
                                                        <td width="70" runat="server" id="fldAutoQcNumberheader" class="field_title">
                                                                <asp:Label ID="lblqualitycontrol" runat="server" Text="QA No"></asp:Label>
                                                            </td>
                                                            <td runat="server" id="fldAutoQcNumber" width="70" class="field_input" colspan="3">
                                                                <asp:TextBox ID="txtqcno" runat="server" placeholder="Auto Generate Quality Assurance No"
                                                                    Width="180" Enabled="false"></asp:TextBox>
                                                            </td>

                                                         <td width="110" class="field_title" style="padding-left: 10px;">
                                                                <asp:Label ID="Label7" runat="server" Text="Ref No"></asp:Label>
                                                            </td>
                                                             <td width="150" class="field_input">
                                                            <asp:TextBox ID="txtrefno" runat="server" ValidationGroup="Submit" AutoCompleteType="Disabled" Enabled="false"  Width="180"> 
                                                            </asp:TextBox>
                                                                 </td>
                                                     </tr>

                                                     <tr>
                                                             <td width="110" class="field_title" style="padding-left: 10px;">
                                                                <asp:Label ID="Label1" runat="server" Text="Entry Date"></asp:Label><span class="req">*</span>
                                                            </td>
                                                           
                                                             <td width="135" class="field_input">
                                                                <asp:TextBox ID="txtentryDate" runat="server" Width="100" Enabled="false" placeholder="dd/mm/yyyy"
                                                                    MaxLength="10" ValidationGroup="Submit" Font-Bold="true" onkeypress="return isNumberKeyWithslash(event);"></asp:TextBox>
                                                                <asp:ImageButton ID="imgPopuppodate" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                    runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender5" PopupButtonID="imgPopuppodate"
                                                                    runat="server" TargetControlID="txtentryDate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                                <asp:RequiredFieldValidator ID="CV_entryDate" runat="server" ControlToValidate="txtentryDate"
                                                                    ValidationGroup="Submit" SetFocusOnError="true" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender17" runat="server"
                                                                    TargetControlID="CV_entryDate" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="../images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                            </td>
                                                             <td width="110" class="field_title" style="padding-left: 10px;">
                                                                <asp:Label ID="Label6" runat="server" Text="Sample Date"></asp:Label><span class="req">*</span>
                                                            </td>

                                                             <td width="145" class="field_input">
                                                                <asp:TextBox ID="txtsampledate" runat="server" Width="100" Enabled="false" placeholder="dd/mm/yyyy"
                                                                    MaxLength="10" ValidationGroup="Submit" Font-Bold="true" onkeypress="return isNumberKeyWithslash(event);"></asp:TextBox>
                                                                <asp:ImageButton ID="ImageButton2" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                    runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImageButton2"
                                                                    runat="server" TargetControlID="txtsampledate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                                <asp:RequiredFieldValidator ID="rfv_txtsampledate" runat="server" ControlToValidate="txtsampledate"
                                                                    ValidationGroup="Submit" SetFocusOnError="true" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                                                    TargetControlID="rfv_txtsampledate" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="../images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                            </td>
                                                        </tr>                                                       
                                                        <tr>
                                                             <td width="110" class="field_title" style="padding-left: 10px;">
                                                                <asp:Label ID="Label8" runat="server" Text="QC Date"></asp:Label><span class="req">*</span>
                                                            </td>

                                                             <td width="145" class="field_input">
                                                                <asp:TextBox ID="txtqcdate" runat="server" Width="100" Enabled="false" placeholder="dd/mm/yyyy"
                                                                    MaxLength="10" ValidationGroup="Submit" Font-Bold="true" onkeypress="return isNumberKeyWithslash(event);"></asp:TextBox>
                                                                <asp:ImageButton ID="ImageButton3" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                    runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImageButton3"
                                                                    runat="server" TargetControlID="txtqcdate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="txtQCDate"
                                                                    ValidationGroup="Submit" SetFocusOnError="true" ErrorMessage="Required" ForeColor="Red">
                                                                </asp:RequiredFieldValidator>
                                                                 <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender32" runat="server"
                                                                    TargetControlID="RequiredFieldValidator22" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="../images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                               
                                                            </td>
                                                        </tr>
                                                            <tr>                                                  
                                                            <td width="70" runat="server"  class="field_title">
                                                                <asp:Label ID="Label911" runat="server" Text="GRN NO"></asp:Label>
                                                                <span class="req">*</span>
                                                                
                                                            </td>
                                                           <td width="145" class="field_input">
                                                                <asp:DropDownList ID="ddlgrno" Width="235" runat="server" class="chosen-select"
                                                                   OnSelectedIndexChanged="ddlgrno_SelectedIndexChanged" AutoPostBack="true" ValidationGroup="Submit">
                                                               </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfv_ddlgrno" runat="server" ControlToValidate="ddlgrno"
                                                                    InitialValue="0" SetFocusOnError="true" ErrorMessage="required!" ForeColor="Red"
                                                                    ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                            </td>

                                                                <td width="90" class="field_title">
                                                                <asp:Label ID="Label2" runat="server" Text="Vendor"></asp:Label>
                                                            </td>
                                                            <td width="250" class="field_input">
                                                                <asp:DropDownList ID="ddlVendor" Width="235" runat="server" ValidationGroup="Submit"
                                                                class="chosen-select"
                                                                AutoPostBack="true"></asp:DropDownList>
                                                                    
                                                                <asp:RequiredFieldValidator ID="CV_TPUNAME" runat="server" ControlToValidate="ddlVendor"
                                                                    InitialValue="0" SetFocusOnError="true" ErrorMessage="required!" ForeColor="Red"
                                                                    ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                            </td>   

                                                             <td width="70" runat="server"  class="field_title">
                                                               <asp:Label ID="lblgrndate" runat="server" Text="GRN DATE"></asp:Label>
                                                            </td>

                                                             <td width="250" class="field_input">
                                                               <asp:TextBox ID="txtgrndate" runat="server" Width="100" Enabled="false" placeholder="dd/mm/yyyy"
                                                                    MaxLength="10" ValidationGroup="Submit" Font-Bold="true" onkeypress="return isNumberKeyWithslash(event);"></asp:TextBox>
                                                                <asp:ImageButton ID="ImageButton4" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                    runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender4" PopupButtonID="ImageButton4"
                                                                    runat="server" TargetControlID="txtgrndate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                                <asp:RequiredFieldValidator ID="rfv_txtgrndate" runat="server" ControlToValidate="txtgrndate"
                                                                    ValidationGroup="Submit" SetFocusOnError="true" ErrorMessage="Required" ForeColor="Red">
                                                                </asp:RequiredFieldValidator>
                                                                 <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                                                    TargetControlID="rfv_txtgrndate" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="../images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                                
                                                            </td>
                                                            </tr>
                                                            <tr>
                                                             <td id="Td4" width="70" runat="server" class="field_title">
                                                               <asp:Label ID="lblfactory" runat="server" Text="FACTORY"></asp:Label>
                                                            </td>
                                                            <td>
                                                            <asp:TextBox ID="txtfactory" runat="server" Width="380" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                        
                                    </table>
                                    <div class="gridcontent-inner" id="divqcdetails" runat="server">
                                        <fieldset>
                                            <legend>CHECKED QUALITY ASSURANCE DETAILS</legend>
                                           
                                               
                                                    <div style="margin: 0 auto; width: 100%;">
                                                        <div style="overflow: hidden;" id="DivHeaderRow" style="position: absolute">
                                                        </div>
                                                        <div id="DivMainContent" style="overflow: scroll; height: 300px;">
                                                            <asp:GridView ID="gvproductdetails" runat="server" Width="100%" CssClass="zebra" AutoGenerateColumns="false"
                                                                EmptyDataText="No Records Available"   onrowdatabound="gvproductdetails_RowDataBound">
                                                                <Columns>


                                                                    <asp:TemplateField HeaderText="SL No.">
                                                                        <ItemTemplate>
                                                                            <%#Container.DataItemIndex+1%>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="2%" />
                                                                    </asp:TemplateField>
                                                                  
                                                                   <asp:TemplateField HeaderText="PRODUCTID" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPRODUCTID" runat="server"  Text='<%# Bind("PRODUCTID") %>'
                                                                                value='<%# Eval("PRODUCTID") %>' ></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="PRODUCTNAME" >
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPRODUCTNAME" runat="server" Text='<%# Bind("PRODUCTNAME") %>'
                                                                                value='<%# Eval("PRODUCTNAME") %>' ></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="PACKINGSIZEID" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPACKINGSIZEID" runat="server" Text='<%# Bind("UNITID") %>'
                                                                                value='<%# Eval("UNITID") %>' ></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Unit" >
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPACKINGSIZENAME" runat="server" Text='<%# Bind("UNITNAME") %>'
                                                                                value='<%# Eval("UNITNAME") %>' ></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="RECEIVEDQTY" >
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblRECEIVEDQTY" runat="server" Text='<%# Bind("RECEIVEDQTY") %>'
                                                                                value='<%# Eval("RECEIVEDQTY") %>' ></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="BATCHNO" >
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblBATCHNO" runat="server" Text='<%# Bind("BATCHNO") %>'
                                                                                value='<%# Eval("BATCHNO") %>'> </asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="INVOICENO" >
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblINVOICENO" runat="server" Text='<%# Bind("INVOICENO") %>'
                                                                                value='<%# Eval("INVOICENO") %>'> </asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="INVOICEDATE" >
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblINVOICEDATE" runat="server" Text='<%# Bind("INVOICEDATE") %>'
                                                                                value='<%# Eval("INVOICEDATE") %>'> </asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="GATEPASSNO" >
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblGATEPASSNO" runat="server" Text='<%# Bind("GATEPASSNO") %>'
                                                                                value='<%# Eval("GATEPASSNO") %>'> </asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="MFG DATE" >
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblMFDATE" runat="server" Text='<%# Bind("MFDATE") %>'
                                                                                value='<%# Eval("MFDATE") %>' ></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="EXPR DATE" >
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblEXPRDATE" runat="server" Text='<%# Bind("EXPRDATE") %>'
                                                                                value='<%# Eval("EXPRDATE") %>' ></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                   
                                                                    <asp:TemplateField HeaderText="SAMPLE QTY" ItemStyle-Width="50px" HeaderStyle-Width="50px">
                                                                        <ItemTemplate>
                                                                           
                                                                            <asp:TextBox ID="txtsampleqty" runat="server" Text='<%# Bind("SAMPLEQTY") %>' 
                                                                                value='<%# Eval("SAMPLEQTY") %>' onkeypress="return isNumberKeyWithDot(event);" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                     <asp:TemplateField HeaderText="QC QUALIFIED Qty" ItemStyle-Width="50px" HeaderStyle-Width="50px">
                                                                        <ItemTemplate>
                                                                           
                                                                            <asp:TextBox ID="txtQCQUALIFIEDQty" runat="server" Text='<%# Bind("QCQUALIFIEDQty") %>'
                                                                             value='<%# Eval("QCQUALIFIEDQty") %>' onkeypress="return isNumberKeyWithDot(event);" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Return Qty" ItemStyle-Width="50px" HeaderStyle-Width="50px">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtReturnQty" runat="server" Text='<%# Bind("ReturnQty") %>' 
                                                                            value='<%# Eval("ReturnQty") %>' onkeypress="return isNumberKeyWithDot(event);" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                     <asp:TemplateField HeaderText="HOLD QTY" ItemStyle-Width="50px" HeaderStyle-Width="50px">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtHOLDQTY" runat="server" Text='<%# Bind("HOLDQTY") %>' 
                                                                            value='<%# Eval("HOLDQTY") %>' onkeypress="return isNumberKeyWithDot(event);" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                     <asp:TemplateField HeaderText="REJECTED QTY" ItemStyle-Width="50px" HeaderStyle-Width="50px">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtREJECTEDQTY" runat="server" Text='<%# Bind("REJECTEDQTY") %>' 
                                                                            value='<%# Eval("REJECTEDQTY") %>' onkeypress="return isNumberKeyWithDot(event);"/>
                                                                        </ItemTemplate>                                                                    </asp:TemplateField>
                                                                    
                                                                    <asp:TemplateField HeaderText="REAJECTION REASON">
                                                                        
                                                                        <ItemTemplate>
                                                                          

                                                                            <asp:DropDownList ID="ddlREAJECTIONREASON" runat="server" Width="260px">
                                                                            </asp:DropDownList>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                        <div id="DivFooterRow" style="overflow: hidden;">
                                                        </div>
                                                    </div>
                                               
                                           
                                        </fieldset>
                                
                                    </div>
                                    
                                 
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">  
                                    <tr>                                                                             
                                       <td width="90" id="Td2"  class="field_title" style="padding-left: 10px;" runat="server" >
                                                <asp:Label ID="chk" runat="server" Text="Remarks" ForeColor="Blue"></asp:Label>
                                       </td>
                                        <td width="200" class="field_input">
                                                <asp:TextBox ID="txtremarks" runat="server" MaxLength="255" TextMode="MultiLine" 
                                            placeholder="Remarks" ></asp:TextBox>
                                                        
                                        </td>
                                        <td id="Td1" width="110" class="field_title" style="padding-left: 10px;" runat="server" >
                                                <asp:Label ID="Label4" runat="server" Text="Rating(%)" ForeColor="Blue"></asp:Label>
                                       </td>
                                        <td width="120" class="field_input">
                                                <asp:TextBox ID="txtrating" runat="server" MaxLength="150" TextMode="MultiLine" Width="150"
                                             ></asp:TextBox>
                                                        
                                        </td>
                                        
                                    </tr> 
                                    <tr>

                                    <td  class="field_title" style="padding-left: 10px;" runat="server" id="Td3">
                                                <asp:Label ID="Label9" runat="server" Text="QC DOCUMENT UPLOAD" ForeColor="Blue"></asp:Label>
                                            </td>
                                            <td align="left" class="field_input" style="padding-bottom:20px;" width="10">
                                                <asp:CheckBox ID="chkfileupload" runat="server" Text=" " OnCheckedChanged="chkfileupload_check" 
                                                        CausesValidation="false" AutoPostBack="true" />
                                            </td>
                                            <td  class="field_input">
                                                <div class="btn_24_blue" id="divshow" runat="server" >
                                                    <span class="icon exclamation_co"></span>
                                                    <asp:Button ID="btnshow" runat="server" Text="Documents" CssClass="btn_link"
                                                        CausesValidation="false" OnClick="btnshow_Click" />
                                                </div>
                                            </td>
                                         <td  class="field_title" style="padding-left: 10px;" runat="server" id="Td5">
                                                <asp:Label ID="Label3" runat="server" Text="COA Document Upload" ForeColor="Blue"></asp:Label>
                                            </td>
                                            <td align="left" class="field_input" style="padding-bottom:20px;" width="50" runat="server" id="ckhidtd">
                                                <asp:CheckBox ID="chkfileuploadCOA" runat="server" Text=" " OnCheckedChanged="chkfileupload_check_COA" 
                                                        CausesValidation="false" AutoPostBack="true" />
                                            </td>
                                            <td  class="field_input">
                                                <div class="btn_24_blue" id="divshowCOA" runat="server" >
                                                    <span class="icon exclamation_co"></span>
                                                    <asp:Button ID="btnshowCOA" runat="server" Text="Documents" CssClass="btn_link"
                                                        CausesValidation="false" OnClick="btnshow_Click_COA" />
                                                </div>
                                            </td>
                                            </tr>
                                            <tr>
                                    <td  class="field_input" colspan="3">
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_blue" id="divbtnsave" runat="server">
                                                    <span class="icon disk_co"></span>
                                                    <asp:Button ID="btnsave" runat="server" Text="Save" CssClass="btn_link" OnClick="btnsave_Click" ValidationGroup="Submit"/>
                                                </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_blue" id="divbtncancel" runat="server">
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
                                                 <div class="btn_24_red" id="divbtnrejection" runat="server">
                                                                <span class="icon reject_co"></span>
                                                                <asp:LinkButton ID="btnreject" runat="server" Text="Reject" CssClass="btn_link"
                                                                    CausesValidation="false" OnClick="btnreject_Click" />
                                                            </div>
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
                                                            <asp:Label ID="Label5" runat="server" Text="From Date"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td width="165">
                                                            <asp:TextBox ID="txtfromdateins" runat="server" MaxLength="10" Width="120" Enabled="false"
                                                                placeholder="dd/mm/yyyy" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="datecheckins"
                                                                Font-Bold="true"></asp:TextBox>
                                                            <asp:ImageButton ID="imgPopup" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="Calendar1" PopupButtonID="imgPopup" runat="server"
                                                                TargetControlID="txtfromdateins" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <asp:RequiredFieldValidator ID="CV_FromDate_QC" runat="server" ControlToValidate="txtfromdateins"
                                                                SetFocusOnError="true" ErrorMessage="From Date is required!" Display="None" ValidationGroup="datecheckins"></asp:RequiredFieldValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server"
                                                                TargetControlID="CV_FromDate_QC" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                            <asp:CustomValidator ID="CV_FromDateValidation_qc" runat="server" ClientValidationFunction="ValidateDate"
                                                                ControlToValidate="txtfromdateins" SetFocusOnError="true" ErrorMessage="* Invalid Date!"
                                                                Display="None" ValidationGroup="datecheckins" />
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender28" runat="server"
                                                                TargetControlID="CV_FromDateValidation_qc" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                        </td>
                                                        <td width="70">
                                                            <asp:Label ID="Label11" runat="server" Text="To Date"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td width="165">
                                                            <asp:TextBox ID="txttodateins" runat="server" MaxLength="10" Width="120" Enabled="false"
                                                                placeholder="dd/mm/yyyy" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="datecheckins"
                                                                Font-Bold="true"></asp:TextBox>
                                                            <asp:ImageButton ID="ImageButton1" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="ImageButton1"
                                                                runat="server" TargetControlID="txttodateins" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <asp:RequiredFieldValidator ID="CV_ToDate_QC" runat="server" ControlToValidate="txttodateins"
                                                                SetFocusOnError="true" ErrorMessage="To Date is required!" Display="None" ValidationGroup="datecheckins"></asp:RequiredFieldValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server"
                                                                TargetControlID="CV_ToDate_QC" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                            <asp:CustomValidator ID="CV_ToDateValidation_QC" runat="server" ClientValidationFunction="ValidateDate"
                                                                ControlToValidate="txttodateins" SetFocusOnError="true" ErrorMessage="* Invalid Date!"
                                                                Display="None" ValidationGroup="datecheckins" />
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server"
                                                                TargetControlID="CV_ToDateValidation_QC" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                        </td>
                                                        <td>
                                                            <div class="btn_24_blue">
                                                                <span class="icon find_co"></span>
                                                                <asp:Button ID="btnQCfind" runat="server" Text="Search" CssClass="btn_link" ValidationGroup="datecheckins"
                                                                    OnClick="btnQCfind_Click" />
                                                            </div>
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
                                        <cc1:Grid ID="gvQcdetails" runat="server" CallbackMode="true" Serialize="true" PageSize="200" EnableRecordHover="true"
                                            FolderStyle="../GridStyles/premiere_blue" AutoGenerateColumns="false" AllowColumnResizing="true"
                                            AllowAddingRecords="false" AllowFiltering="true"  OnRowDataBound="gvQcdetails_RowDataBound" >
                                            <FilteringSettings InitialState="Visible" />
                                            <Columns>
                                                <cc1:Column ID="Column20" DataField="Sl" HeaderText="SL" runat="server" Width="60">
                                                    <TemplateSettings TemplateId="slnoTemplateIN" />
                                                </cc1:Column>
                                                <cc1:Column ID="Column1" DataField="QCID" HeaderText="QCID" runat="server" Width="120" Wrap="true" Visible="false">
                                                    
                                                </cc1:Column>
                                                
                                                <cc1:Column ID="Column22" DataField="QCNO" HeaderText="QA NO" runat="server" Width="220" Wrap="true">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="EqualTo" />
                                                        <cc1:FilterOption Type="Contains" />
                                                       
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column21" DataField="QCDATE" HeaderText="QA DATE" runat="server" Width="100" Wrap="true">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="EqualTo" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column24" DataField="REFNO" HeaderText="REF NO" runat="server"  Width="150" Wrap="true">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="EqualTo" />
                                                        <cc1:FilterOption Type="Contains" />
                                                       
                                                    </FilterOptions>
                                                </cc1:Column>
                                               
                                                <cc1:Column ID="Column40" DataField="GRNNUMBER" HeaderText="GRN NUMBER" runat="server" Width="200" Wrap="true">
                                                </cc1:Column>

                                                <cc1:Column ID="Column38" DataField="VENDORNAME" HeaderText="VENDOR NAME" runat="server" Width="200" Wrap="true">
                                                </cc1:Column>
                                                <cc1:Column ID="Column39" DataField="NEXTLEVELID" HeaderText="NEXTLEVELID" runat="server" Width="80" Wrap="true" Visible="false">
                                                </cc1:Column>
                                                 <cc1:Column ID="Column2" DataField="ISVERIFIED" HeaderText="ISVERIFIED" runat="server" Width="80" Wrap="true" Visible="false">
                                                </cc1:Column>
                                                 <cc1:Column ID="Column3" DataField="APPROVEDDESC" HeaderText="APPROVED STATUS" runat="server" Width="100" Wrap="true" >
                                                </cc1:Column>
                                                <cc1:Column ID="Column44" DataField="CREATEDBY" HeaderText="CREATEDBY" runat="server" Width="110" Wrap="true" >
                                                </cc1:Column>
                                                 <cc1:Column ID="Column46" DataField="APPROVEDBY" HeaderText="APPROVEDBY" runat="server" Width="110" Wrap="true" >
                                                </cc1:Column>
                                                <cc1:Column ID="Column41" AllowEdit="true" AllowDelete="true" HeaderText="VIEW" runat="server" Wrap="true"
                                                    Width="70" Visible="false">
                                                    <TemplateSettings TemplateId="viewBtnTemplate" />
                                                </cc1:Column>
                                                <cc1:Column ID="Column23" AllowEdit="true" AllowDelete="true" HeaderText="EDIT" runat="server" Wrap="true"
                                                    Width="70">
                                                    <TemplateSettings TemplateId="editBtnTemplate" />
                                                </cc1:Column>
                                                <cc1:Column ID="Column42" AllowEdit="true" AllowDelete="true" HeaderText="DELETE" runat="server" Wrap="true" Width="80">
                                                    <TemplateSettings TemplateId="deleteqcBtnTemplate" />
                                                </cc1:Column>
                                            </Columns>
                                            <FilteringSettings MatchingType="AnyFilter" />
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="editBtnTemplate">
                                                    <Template>
                                                        <%--<asp:Button runat="server" ID="btn1" CssClass="action-icons c-edit" Text="Edit" OnCommand="EditRecord"  CommandArgument= '<%# Eval("[USERID]") %>' />--%>
                                                        <a href="javascript: //" class="action-icons c-edit" id="btnGridEdit_<%# Container.PageRecordIndex %>"
                                                            onclick="CallServerMethod(this)"></a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="deleteqcBtnTemplate">
                                                    <Template>
                                                        <a href="javascript: //" class="action-icons c-delete" id="btnGridQCDelete_<%# Container.PageRecordIndex %>"
                                                            onclick="CallQCDeleteServerMethod(this)"></a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="viewBtnTemplate">
                                                    <Template>
                                                        <%--<asp:Button runat="server" ID="btn1" CssClass="action-icons c-edit" Text="Edit" OnCommand="EditRecord"  CommandArgument= '<%# Eval("[USERID]") %>' />--%>
                                                        <a href="javascript: //" class="h_icon zoom_sl" id="btnGridView_<%# Container.PageRecordIndex %>"
                                                            onclick="CallViewServerMethod(this)"></a>
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
                                            <ScrollingSettings ScrollWidth="100%" ScrollHeight="290" />
                                        </cc1:Grid>
                                        <asp:Button ID="btngrdedit" runat="server" Text="grdedit" Style="display: none" OnClick="btngrdedit_Click"
                                            CausesValidation="false" />
                                        <asp:Button ID="btngrdqcdelete" runat="server" Text="Delete" Style="display: none" OnClick="btngrdqcdelete_Click"
                                            CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete this record?');" />
                                        <asp:Button ID="btngrdview" runat="server" Text="grdedit" Style="display: none" OnClick="btngrdview_Click"
                                            CausesValidation="false" />
                                             <asp:HiddenField ID="hdn_qcno" runat="server" />
                                             <asp:HiddenField ID="hdnqcid" runat="server" />
                                    </div>
                                </asp:Panel>
                                <script type="text/javascript">
                                    function isNumberKeyWithDot(evt) {
                                        var charCode = (evt.which) ? evt.which : event.keyCode;
                                        if ((charCode < 48 || charCode > 57) && (charCode != 8) && (charCode != 46))
                                            return false;

                                        return true;
                                    }

                                    function CallServerMethod(oLink) {
                                        var iRecordIndex = oLink.id.toString().replace("btnGridEdit_", "");
                                        document.getElementById("<%=hdnqcid.ClientID %>").value = gvQcdetails.Rows[iRecordIndex].Cells[1].Value;
                                        document.getElementById("<%=btngrdedit.ClientID %>").click();

                                    }

                                    function CallQCDeleteServerMethod(oLink) {
                                        var iRecordIndex = oLink.id.toString().replace("btnGridQCDelete_", "");
                                        document.getElementById("<%=hdnqcid.ClientID %>").value = gvQcdetails.Rows[iRecordIndex].Cells[1].Value;
                                        document.getElementById("<%=btngrdqcdelete.ClientID %>").click();
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
                                        gvCurrentQcdetails.addFilterCriteria('QCDATE', OboutGridFilterCriteria.Contains, searchValue);
                                        gvCurrentQcdetails.addFilterCriteria('QCNO', OboutGridFilterCriteria.Contains, searchValue);
                                        gvCurrentQcdetails.addFilterCriteria('TPUNAME', OboutGridFilterCriteria.Contains, searchValue);
                                        gvCurrentQcdetails.executeFilter();
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
                                <cc2:MessageBox ID="MessageBox1" runat="server" /> 
                            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>