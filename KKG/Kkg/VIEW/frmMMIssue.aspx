<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmMMIssue.aspx.cs" Inherits="VIEW_frmMMIssue" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function pageload(sender, args) {
            //calculation();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        th {
            background: cornflowerblue !important;
            color: white !important;
            position: sticky !important;
            top: 0;
            box-shadow: 0 2px 2px -1px rgb(255, 106, 0);
            width: 400px;
        }

        th, td {
            padding: 0.25rem;
        }
    </style> 

     <style type="text/css">  
        .Background  
        {  
            background-color: Black;  
            filter: alpha(opacity=90);  
            opacity: 0.8;  
        }  
        .Popup  
        {  
            background-color: #FFFFFF;  
            border-width: 3px;  
            border-style: solid;  
            border-color: black;  
            padding-top: 10px;  
            padding-left: 10px;  
            width: 1000px;  
            height: 350px;  
        }  
        .lbl  
        {  
            font-size:16px;  
            font-style:italic;  
            font-weight:bold;  
        }  
    </style>  

        
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
    <script type="text/javascript">
        function CheckOne(obj) {
            var grid = obj.parentNode.parentNode.parentNode;
            var inputs = grid.getElementsByTagName("input");
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].type == "checkbox") {
                    if (obj.checked && inputs[i] != obj && inputs[i].checked) {
                        inputs[i].checked = false;
                    }
                }
            }
        }


         function HighlightRow(chkB) {

            var IsChecked = chkB.checked;

            if (IsChecked) {

                chkB.parentElement.parentElement.style.backgroundColor = 'PaleGreen';

                chkB.parentElement.parentElement.style.color = 'black';

            } else {

                chkB.parentElement.parentElement.style.backgroundColor = 'PowderBlue';

                chkB.parentElement.parentElement.style.color = 'black';

            }

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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
               

            <%--<div class="page_title">
                <span class="title_icon"><span class="computer_imac"></span></span>
                <h3>Purchase Order</h3>
            </div>--%>
            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active">
                                <span class="h_icon_he list_images"></span>
                                <h6>ISSUE Details</h6>
                                <div id="divadd" runat="server" class="btn_30_light" style="float: right;">
                                    <span id="icon" class="icon add_co"></span>
                                    <asp:Button ID="btnnewentry" runat="server" Text="Add New Record" CssClass="btn_link"
                                        OnClick="btnnewentry_Click" CausesValidation="false" />
                                </div>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="InputTable" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr id="divpono" runat="server">
                                            <td class="field_input" style="padding-left: 10px;">
                                                <fieldset>
                                                    <legend>ISSUE NO </legend>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td width="110" class="innerfield_title">
                                                                <asp:Label ID="Label15" runat="server" Text="ISSUE NO"></asp:Label>
                                                            </td>
                                                            <td width="250">
                                                                <asp:TextBox ID="txtissueno" runat="server" CssClass="large" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td>&nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_input" style="padding-left: 10px;">
                                                <fieldset>
                                                    <legend>ISSUE DETAILS</legend>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                        <tr runat="server" id="trAutoIssueNo">
                                                            <td class="field_title">
                                                                <asp:Label ID="lblIssueNo" Text="IssueNo" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input" colspan="5">
                                                                <asp:TextBox ID="txtShowIssueNo" runat="server" Width="200" placeholder="Auto Generate No."
                                                                    Enabled="false" Font-Bold="true"> </asp:TextBox>
                                                        </tr>
                                                        <tr>
                                                            <td width="90" class="innerfield_title">
                                                                <asp:Label ID="Label11" runat="server" Text="Entry Date"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td width="165">
                                                                <asp:TextBox ID="txtissuedate" runat="server" Width="120" placeholder="dd/mm/yyyy"
                                                                    MaxLength="10" ValidationGroup="datecheckpodetail" Enabled="false" onkeypress="return isNumberKeyWithslash(event);"></asp:TextBox>
                                                                <asp:ImageButton ID="imgPopuppodate" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                                    runat="server" Height="24" Enabled="true" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" PopupButtonID="imgPopuppodate"
                                                                    runat="server" TargetControlID="txtissuedate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                                <asp:RequiredFieldValidator ID="rfv_txtissue" runat="server" ControlToValidate="txtissuedate"
                                                                    ValidationGroup="datecheckissuedetail" SetFocusOnError="true" ErrorMessage="ISSUE Date is required!"
                                                                    Display="None"></asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender17" runat="server"
                                                                    TargetControlID="rfv_txtissue" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="~/images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                            </td>
                                                            <td width="110" class="innerfield_title">
                                                                <asp:Label ID="Label5" runat="server" Text="Own Department"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td width="160">
                                                                <asp:DropDownList ID="ddldepartment" runat="server" Width="145px" Height="28px" ValidationGroup="datecheckrequision"
                                                                    class="chosen-select" data-placeholder="-- SELECT DEPARTMENT NAME --">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfv_datecheckissuedetail" runat="server" ControlToValidate="ddldepartment"
                                                                    InitialValue="0" SetFocusOnError="true" ErrorMessage="Required!" ForeColor="Red"
                                                                    ValidationGroup="datecheckrequision"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td width="110" class="innerfield_title">
                                                                <asp:Label ID="Label1" runat="server" Text="Destination Department"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td width="160">
                                                                <asp:DropDownList ID="ddlRequistionDepartMent" runat="server" Width="145px" Height="28px" ValidationGroup="datecheckrequision" AutoPostBack="true"
                                                                    class="chosen-select" data-placeholder="-- SELECT DEPARTMENT NAME --" OnSelectedIndexChanged="ddlRequistionDepartMent_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlRequistionDepartMent"
                                                                    InitialValue="0" SetFocusOnError="true" ErrorMessage="Required!" ForeColor="Red"
                                                                    ValidationGroup="search"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td width="110" class="innerfield_title">
                                                                <asp:Label ID="lblStore" runat="server" Text="To Storelocation"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td width="160">
                                                                <asp:DropDownList ID="ddlStore" runat="server" Width="145px" Height="28px" ValidationGroup="datecheckrequision"
                                                                    class="chosen-select" data-placeholder="-- SELECT STORE NAME --">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorStore" runat="server" ControlToValidate="ddlStore"
                                                                    InitialValue="0" SetFocusOnError="true" ErrorMessage="Required!" ForeColor="Red"
                                                                    ValidationGroup="datecheckrequision"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="90" class="innerfield_title">
                                                                <asp:Label ID="Label14" runat="server" Text="From Date"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td width="165">
                                                                <asp:TextBox ID="txtrequifromdate" runat="server" Width="80" placeholder="dd/mm/yyyy"
                                                                    MaxLength="10" ValidationGroup="datecheckrequision" Enabled="false" onkeypress="return isNumberKeyWithslash(event);"></asp:TextBox>
                                                                <asp:ImageButton ID="imgPopuppodatetxtrequifromdate" ImageUrl="~/images/calendar.png"
                                                                    ImageAlign="AbsMiddle" runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender8" PopupButtonID="imgPopuppodatetxtrequifromdate"
                                                                    runat="server" TargetControlID="txtrequifromdate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                                <asp:RequiredFieldValidator ID="rfv_txtrequifromdate" runat="server" ControlToValidate="txtrequifromdate"
                                                                    ValidationGroup="datecheckrequision" SetFocusOnError="true" ErrorMessage="ISSUE Date is required!"
                                                                    Display="None"></asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                                                    TargetControlID="rfv_txtrequifromdate" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="~/images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                            </td>
                                                            <td width="90" class="innerfield_title">
                                                                <asp:Label ID="Label18" runat="server" Text="To Date"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td width="165">
                                                                <asp:TextBox ID="txtrequitodate" runat="server" Width="80" placeholder="dd/mm/yyyy"
                                                                    MaxLength="10" ValidationGroup="datecheckrequision" Enabled="false" onkeypress="return isNumberKeyWithslash(event);"></asp:TextBox>
                                                                <asp:ImageButton ID="ImageButtontxtrequitodate" ImageUrl="~/images/calendar.png"
                                                                    ImageAlign="AbsMiddle" runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender9" PopupButtonID="ImageButtontxtrequitodate"
                                                                    runat="server" TargetControlID="txtrequitodate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                                <asp:RequiredFieldValidator ID="rfv_txtrequitodate" runat="server" ControlToValidate="txtrequitodate"
                                                                    ValidationGroup="datecheckrequision" SetFocusOnError="true" ErrorMessage="Requisition Date is required!"
                                                                    Display="None"></asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                                                                    TargetControlID="rfv_txtrequitodate" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="~/images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                            </td>
                                                            <td class="field_titleTr" style="display:none">
                                                                <asp:Label ID="lblfactory" runat="server" Text="Factory"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlfactory" runat="server" AppendDataBoundItems="true" class="chosen-select"
                                                                    Style="width: 250px;" data-placeholder="Choose a Factory" ValidationGroup="datecheckrequision">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="Reqddlfactory" runat="server" ControlToValidate="ddlfactory"
                                                                    InitialValue="0" SetFocusOnError="true" ErrorMessage="*" ForeColor="Red" ValidationGroup="datecheckrequision"></asp:RequiredFieldValidator>
                                                            </td>

                                                              <td class="field_titleTr">
                                                                  Requisition From
                                                                <span class="req">*</span>
                                                              </td>
                                                             <td>
                                                                 <asp:DropDownList ID="ddlRequisitionFrom" runat="server" AppendDataBoundItems="true" class="chosen-select"
                                                                    Style="width: 250px;" data-placeholder="Choose a Type" ValidationGroup="datecheckrequision">
                                                                      <asp:ListItem Value="ALL" Selected="True">ALL REQUISITION</asp:ListItem>
                                                                      <asp:ListItem Value="JBO">JOB ORDER</asp:ListItem>
                                                                </asp:DropDownList>
                                                                 </td>
                                                            <td class="field_title">
                                                                <div class="btn_24_blue">
                                                                    <span class="icon find_co"></span>
                                                                    <asp:Button ID="btnrequisitionsearch" runat="server" Text="Search" CssClass="btn_link"
                                                                        ValidationGroup="search" OnClick="btnrequisitionsearch_Click" CausesValidation="false" />
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_input" style="padding-left: 10px;">
                                                <fieldset>
                                                    <legend>REQUISITION DETAILS</legend>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td class="field_input">
                                                                <div style="overflow: scroll; height: 300px;">
                                                                    <asp:GridView ID="gvrequistiondetails" EmptyDataText="There are no records available."
                                                                        CssClass="zebra" runat="server" AutoGenerateColumns="False" Width="100%">
                                                                        <PagerSettings Position="TopAndBottom" />
                                                                        <PagerStyle HorizontalAlign="Left" />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="" HeaderStyle-Width="20">
                                                                               
                                                                                     <HeaderTemplate>
                                                                <asp:CheckBox ID="chkAllSelect" runat="server" onclick="CheckAllheader(this);" Text="Select Top 40 " />
                                                            </HeaderTemplate>
                                                            <HeaderStyle Width="25px" />
                                                                                 <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkreq" runat="server" Text=" " value='<%# Eval("REQUISITIONID") %>'
                                                                                         onclick="javascript:HighlightRow(this);" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="SL" HeaderStyle-Width="20">
                                                                                <ItemTemplate>
                                                                                    <%# Container.DataItemIndex + 1 %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="" HeaderStyle-Width="20" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblREQUISITIONID" runat="server" Text='<%# Eval("REQUISITIONID") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="WORKORDERNO" HeaderText="WORK ORDER NO" HeaderStyle-Wrap="false"></asp:BoundField>
                                                                            <asp:BoundField DataField="REQUISITIONNO" HeaderText="REQUISITION NO" HeaderStyle-Wrap="false"></asp:BoundField>
                                                                            <asp:BoundField DataField="REQUISITIONDATE" HeaderText="REQUISITION DATE" HeaderStyle-Wrap="true"></asp:BoundField>

                                                                            <asp:TemplateField HeaderText=" DESTINATION DEPARTMENT">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblDISDEPTNAME" runat="server" Text='<%# Eval("DESTINATION_DEPTNAME") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField> 

                                                                            <asp:TemplateField HeaderText="BULK/FG" HeaderStyle-Width="280" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("PRODUCTNAME") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="DEPTID" HeaderStyle-Width="20" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblDEPTID" runat="server" Text='<%# Eval("DEPTID") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                          
                                                                            <asp:TemplateField HeaderText="OWN DEPARTMENT">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblDEPTNAME" runat="server" Text='<%# Eval("DEPTNAME") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField> 
                                                                           
                                                                            <asp:BoundField DataField="VENDORNAME" HeaderText="PARTY" Visible="false"></asp:BoundField>
                                                                            <asp:TemplateField ShowHeader="TRUE" HeaderText="PRINT">
                                                                             <ItemTemplate>
                                                                                 <asp:Button ID="btnPrint" runat="server" CausesValidation="false" OnClick="btnPrint_Click"
                                                                                     Text="PRINT" CommandArgument='<%# Eval("REQUISITIONID") %>' />
                                                                             </ItemTemplate>
                                                                         </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                                <asp:HiddenField ID="Hdn_Fld_requisitionid" runat="server" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                            <asp:Label ID="lbltotalcount" runat="server" ForeColor="WindowText"></asp:Label>

                                        </tr>
                                        <tr>
                                            <td class="field_input">
                                            <div class="btn_24_blue" runat="server" id="div5">
                                            <span class="icon page_white_gear_co"></span>
                                                <asp:Button ID="btnGetRequisitionDetails" OnClick="btnGetRequisitionDetails_Click" runat="server"
                                                    CssClass="btn_link" Text="Genarate" />
                                            </div>
                                        </td>
                                        </tr>
                                    </table>
                                    <div id="Div1" class="gridcontent-inner" runat="server">
                                        <fieldset>
                                            <legend>MATERIAL DETAILS</legend>
                                            <div style="overflow: hidden; width: 100%;" id="DivHeaderRow">
                                            </div>
                                            <div style="overflow: scroll; height: 200px;" onscroll="OnScrollDiv(this)" id="DivMainContent">
                                                <asp:GridView ID="gvmaterialdetails" EmptyDataText="There are no records available."
                                                    CssClass="zebra" runat="server" AutoGenerateColumns="False" onchange="MakeStaticHeader('<%= grdclosingdetails.ClientID %>', 200, '100%' , 30 ,false)"
                                                    Width="100%" OnRowDataBound="gvmaterialdetails_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SL">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CATEGORYID" Visible="false" HeaderStyle-Wrap="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCATEGORYID" runat="server" Text='<%# Eval("CATEGORYID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CATEGORY NAME">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCATEGORYNAME" runat="server" Text='<%# Eval("CATEGORYNAME") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MATERIALID" Visible="false" HeaderStyle-Wrap="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMATERIALID" runat="server" Text='<%# Eval("MATERIALID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MATERIALNAME">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMATERIALNAME" runat="server" Text='<%# Eval("MATERIALNAME") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="UOMID" Visible="false" HeaderStyle-Wrap="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUOMID" runat="server" Text='<%# Eval("UOMID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="UOMNAME">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUOMNAME" runat="server" Text='<%# Eval("UOMNAME") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Qc No" HeaderStyle-Width="70" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtqcno" AutoPostBack="false" runat="server" MaxLength="10"
                                                                    Text='<%#Eval("QCNO") %>' Style="text-align: right;" Width="70px" Height="20" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="REQUISITION QTY" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblQTY" runat="server" Text='<%# Eval("QTY") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="REQUISITION QTY">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtRequQty" runat="server" Text='<%# Eval("QTY") %>' Enabled="false"/>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="STOCK QTY">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblStockQTY" runat="server" Text='<%# Eval("Stock") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ALREADY ISSUED QTY" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblissedQTY" runat="server" Text='<%# Eval("AlreadyIssuedQty") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ALREADY ISSUED QTY">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtissedQTY" runat="server" Text='<%# Eval("AlreadyIssuedQty") %>' Enabled="false" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="GRN NO" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="TXTGRNNO" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="BALANCE QTY">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblbalanceQty" runat="server" Text='<%# Eval("BALANCEQTY") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="ISSUE QTY" HeaderStyle-Width="70">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtissueqty" AutoPostBack="true" runat="server" MaxLength="10" 
                                                                    Text='<%#Eval("BALANCEQTY") %>' Style="text-align: right;" Width="70px" Height="20" OnTextChanged="txtissueqty_TextChanged" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rate" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRate" runat="server" Text='<%# Eval("Rate") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField Visible="false" >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblREQUITIONID" runat="server" Text='<%# Eval("REQUISTIONID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                          <asp:TemplateField  HeaderText="REQUISITION NO">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblREQUISITIONNO" runat="server" Text='<%# Eval("REQUISITIONNO") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                         <asp:TemplateField HeaderText="ISCLOSED" HeaderStyle-Width="20">
                                                           <ItemTemplate>
                                                              <asp:TextBox ID="txtClose" AutoPostBack="true" Width="10px" Enabled="false" runat="server" Text='<%# Eval("ISCLOSED") %>'/>
                                                         </ItemTemplate>
                                                    </asp:TemplateField>


                                                         <asp:TemplateField HeaderText="ISCLOSED" HeaderStyle-Width="20">
                                                           <ItemTemplate>
                                                              <asp:CheckBox ID="isclosedChkreq" AutoPostBack="true" OnCheckedChanged="isclosedChkreq_CheckedChanged" runat="server" Text=" " 
                                                               />
                                                         </ItemTemplate>
                                                    </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div id="DivFooterRow" style="overflow: hidden">
                                            </div>
                                        </fieldset>
                                    </div>
                                    <td class="field_input">
                                        <div class="btn_24_blue" runat="server" id="divMaterialDetails">
                                            <span class="icon add_co"></span>
                                            <asp:Button ID="btnadd" runat="server" Text="Add" CssClass="btn_link" CausesValidation="false"
                                                OnClick="btnadd_Click" />
                                        </div>
                                    </td>
                                    <div class="gridcontent-inner">
                                        <fieldset>
                                            <legend>ISSUE ORDER DETAILS</legend>
                                            <div style="overflow: hidden; width: 100%;" id="Div2">
                                            </div>
                                            <div style="overflow: scroll; height: 200px;" onscroll="OnScrollDiv(this)" id="Div3">
                                                <asp:GridView ID="gvissueorderdetails" EmptyDataText="There are no records available."
                                                    CssClass="zebra" runat="server" AutoGenerateColumns="False" onchange="MakeStaticHeader('<%= grdclosingdetails.ClientID %>', 200, '100%' , 30 ,false)"
                                                    Width="100%">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="GUID" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblGUID" runat="server" Text='<%# Eval("GUID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SL">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CATEGORYID" Visible="false" HeaderStyle-Wrap="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblissueCATEGORYID" runat="server" Text='<%# Eval("CATEGORYID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CATEGORY NAME">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblissueCATEGORYNAME" runat="server" Text='<%# Eval("CATEGORYNAME") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MATERIALID" Visible="false" HeaderStyle-Wrap="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblissueMATERIALID" runat="server" Text='<%# Eval("MATERIALID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MATERIALNAME">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblissueMATERIALNAME" runat="server" Text='<%# Eval("MATERIALNAME") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="UOMID" Visible="false" HeaderStyle-Wrap="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblissueUOMID" runat="server" Text='<%# Eval("UOMID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="UOMNAME">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblissueUOMNAME" runat="server" Text='<%# Eval("UOMNAME") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="QC NO"  Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblQCNO" runat="server" Text='<%# Eval("QCNO") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="REQITION QTY">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblissueREQITIONQTY" runat="server" Text='<%# Eval("REQITIONQTY") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ISSUE QTY">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblissueISSUEQTY" runat="server" Text='<%# Eval("ISSUEQTY") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="GRN NO" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblGRNNO" runat="server" Text='<%# Eval("GRNNO") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rate" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRate" runat="server" Text='<%# Eval("Rate") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ISCLOSED" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblIsclosed" runat="server" Text='<%# Eval("ISCLOSED") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                       
                                                        <asp:TemplateField  Visible="false" HeaderText="Requisition Id">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblREQUITIONIDFORISSUE" runat="server" Text='<%# Eval("REQUITIONID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DELETE">
                                                            <ItemTemplate>
                                                                <%-- <asp:ImageButton ID="btngrddelete" runat="server" CssClass="action-icons c-delete"
                                                                    OnClientClick="return confirm('Are you sure you want to Delete?')" ToolTip="Delete"
                                                                    OnClick="btngrddelete_Click" />--%>
                                                                <asp:Button ID="btngrddelete" runat="server" CssClass="action-icons c-delete"
                                                                    OnClientClick="return confirm('Are you sure you want to Delete?')" ToolTip="Delete"
                                                                    OnClick="btngrddelete_Click" />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div id="Div4" style="overflow: hidden">
                                            </div>
                                        </fieldset>
                                    </div>
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td width="320">
                                                <asp:TextBox ID="txtremarks" runat="server" TextMode="MultiLine" Width="100%" placeholder="Remarks"
                                                    class="input_grow" MaxLength="255"></asp:TextBox>
                                            </td>
                                            <td width="15">&nbsp;
                                                <asp:HiddenField ID="Hdn_Fld" runat="server" />
                                            </td>
                                            <td>
                                                <div class="btn_24_blue" id="divbtnsave" runat="server">
                                                    <span class="icon disk_co"></span>
                                                    <asp:Button ID="btnsave" runat="server" Text="Save" CssClass="btn_link" OnClick="btnsave_Click" />
                                                </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_blue" id="divbtncancel" runat="server">
                                                    <span class="icon cross_octagon_co"></span>
                                                    <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn_link" OnClick="btncancel_Click" />
                                                </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_blue" style="display: none;" runat="server" id="div_btnPrint">
                                                    <span class="icon printer_co"></span>
                                                    <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btn_link" />
                                                </div>
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
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlDisplay" runat="server">

                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_title" width="90">
                                                <asp:Label ID="Label9" runat="server" Text="From Date"></asp:Label>
                                            </td>
                                            <td class="field-input" width="165">
                                                <asp:TextBox ID="txtfromdate" runat="server" MaxLength="15" Width="80" Enabled="false"
                                                    placeholder="dd/mm/yyyy" ValidationGroup="datecheck"></asp:TextBox>
                                                <asp:ImageButton ID="imgPopupfromdate" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                    runat="server" Height="24" />
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPopupfromdate"
                                                    runat="server" TargetControlID="txtfromdate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                            <td class="field_title" width="70">
                                                <asp:Label ID="Label10" runat="server" Text="To Date"></asp:Label>
                                            </td>
                                            <td class="field-input" width="165">
                                                <asp:TextBox ID="txttodate" runat="server" MaxLength="10" Width="80" placeholder="dd/mm/yyyy"
                                                    ValidationGroup="datecheck" Enabled="false"></asp:TextBox>
                                                <asp:ImageButton ID="imgpopuptodate" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                    runat="server" Height="24" />
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="imgpopuptodate"
                                                    runat="server" TargetControlID="txttodate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                            <td width="90" id="TDlblststus" runat="server" class="field_title" style="display:none">
                                                <asp:Label ID="lblststus" runat="server" Text="QC Status"></asp:Label>
                                            </td>
                                            <td width="70" id="TDddltatus" runat="server" class="field_input" style="display:none">
                                                <asp:DropDownList ID="ddltatus" runat="server" AppendDataBoundItems="true" class="chosen-select"
                                                    data-placeholder="Choose a Status" Style="width: 170px;">
                                                    <asp:ListItem Value="0">Choose a Status</asp:ListItem>
                                                    <asp:ListItem Value="N">PENDING</asp:ListItem>
                                                    <asp:ListItem Value="Y">APPROVED</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td class="field_title">
                                                <div class="btn_24_blue">
                                                    <span class="icon find_co"></span>
                                                    <asp:Button ID="btngvfill" runat="server" Text="Search" CssClass="btn_link" ValidationGroup="datecheck"
                                                        OnClick="btngvfill_Click" />
                                                </div>
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
                                    <div class="gridcontent" style="width: 100%">
                                        <cc1:Grid ID="gvIssuegrid" runat="server" CallbackMode="true" Serialize="true" AutoGenerateColumns="false"
                                            AllowAddingRecords="false" PageSize="100" AllowPaging="true" AllowFiltering="true"
                                            Width="100%" AllowPageSizeSelection="true" FolderStyle="../GridStyles/premiere_blue"
                                            EnableRecordHover="true">
                                            <FilteringSettings InitialState="Visible" />
                                            <Columns>
                                                <cc1:Column ID="Column4" DataField="Sl" HeaderText="SL" runat="server" Width="80"
                                                    Wrap="true">
                                                    <TemplateSettings TemplateId="slnoTemplatePO" />
                                                </cc1:Column>
                                                <cc1:Column ID="Column1" DataField="ISSUEID" HeaderText="ISSUEID" runat="server"
                                                    Width="150" Wrap="true" Visible="false">
                                                </cc1:Column>
                                                <cc1:Column ID="Column6" DataField="ISSUEDATE" HeaderText="ISSUE DATE" runat="server"
                                                    Width="150" Wrap="true">
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
                                                <cc1:Column ID="Column7" DataField="ISSUENO" HeaderText="ISSUE NO" runat="server"
                                                    Width="280" Wrap="true">
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

                                                <cc1:Column ID="Column2" DataField="PRODUCTIONNO" HeaderText="PRODUCTION NO" runat="server" 
                                                    Width="180" Wrap="true" Visible="false">
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

                                                 <cc1:Column ID="Column5" AllowEdit="true" AllowDelete="true" HeaderText="SHOW PRODUCTION NO" runat="server"
                                                    Width="80" Wrap="true">
                                                    <TemplateSettings TemplateId="showBtnProduction" />
                                                </cc1:Column>

                                                  <cc1:Column ID="Column8" DataField="REQUISITIONNO" HeaderText="REQUISITION NO" runat="server"
                                                    Width="180" Wrap="true" Visible="false">
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

                                                <cc1:Column ID="Column9" AllowEdit="true" AllowDelete="true" HeaderText="SHOW REQUISITION NO" runat="server"
                                                    Width="80" Wrap="true">
                                                    <TemplateSettings TemplateId="showBtnRequisition" />
                                                </cc1:Column>




                                                <cc1:Column ID="Column13" AllowEdit="true" AllowDelete="true" HeaderText="EDIT" runat="server"
                                                    Width="80" Wrap="true">
                                                    <TemplateSettings TemplateId="editBtnTemplate" />
                                                </cc1:Column>
                                                <cc1:Column HeaderText="DELETE" AllowEdit="false" AllowDelete="true" Width="80" Wrap="true">
                                                    <TemplateSettings TemplateId="deleteBtnTemplatePO" />
                                                </cc1:Column>
                                                <cc1:Column ID="Column3" AllowEdit="true" AllowDelete="true" HeaderText="VIEW" runat="server"
                                                    Width="60">
                                                    <TemplateSettings TemplateId="ViewBtnTemplate" />
                                                </cc1:Column>
                                                <cc1:Column ID="Column333" AllowEdit="false" AllowDelete="false" HeaderText="PRINT"
                                                    runat="server" Width="80" Wrap="true">
                                                    <TemplateSettings TemplateId="PrintBtnTemplate" />
                                                </cc1:Column>
                                            </Columns>
                                            <FilteringSettings MatchingType="AnyFilter" />
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="ViewBtnTemplate">
                                                    <Template>
                                                        <a href="javascript: //" class="h_icon zoom_sl" id="btnGridEdit_<%# Container.PageRecordIndex %>"
                                                            onclick="CallServerViewMethod(this)"></a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="editBtnTemplate">
                                                    <Template>
                                                        <a href="javascript: //" class="action-icons c-edit" id="btnGridEdit_<%# Container.PageRecordIndex %>"
                                                            onclick="CallServerMethod(this)"></a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>

                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="showBtnProduction">
                                                    <Template>
                                                        <a href="javascript: //" class="action-icons c-suspend" id="btnShoWProduction_<%# Container.PageRecordIndex %>"
                                                            onclick="ServerMethodforShowingProduction(this)"></a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>

                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="showBtnRequisition">
                                                    <Template>
                                                        <a href="javascript: //" class="action-icons c-suspend" id="btnShoWRequisition_<%# Container.PageRecordIndex %>"
                                                            onclick="ServerMethodforShowingRequisition(this)"></a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>






                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="deleteBtnTemplatePO">
                                                    <Template>
                                                        <a href="javascript: //" class="action-icons c-delete" title="Delete" id="btnGridFinalDelete_<%# Container.PageRecordIndex %>"
                                                            onclick="CallFinalDeleteServerMethod(this)"></a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                                <cc1:GridTemplate runat="server" ID="slnoTemplatePO">
                                                    <Template>
                                                        <asp:Label ID="lblslnoPO" runat="server" Text='<%#Container.PageRecordIndex+1 %>'></asp:Label>
                                                    </Template>
                                                </cc1:GridTemplate>
                                                 <cc1:GridTemplate runat="server" ID="PrintBtnTemplate">
                                                    <Template>
                                                        <a href="javascript: //" class="filter_btn printer_co" id="btnPOPrint_<%# Container.PageRecordIndex %>"
                                                            onclick="CallServerMethodPrint(this)" title="Print"></a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <%--<ScrollingSettings ScrollWidth="100%"  ScrollHeight="290" />--%>
                                        </cc1:Grid>
                                        <asp:Button ID="btngrdedit" runat="server" Text="grdedit" Style="display: none" CausesValidation="false"
                                            OnClick="btngrdedit_Click" />
                                        <asp:Button ID="btnfinalgrdDelete" runat="server" Text="Delete" Style="display: none"
                                            OnClick="btnfinalgrdDelete_Click" CausesValidation="false" OnClientClick="ShowConfirmBox(this,'Are you sure you want to delete this record?'); return false;" />
                                        <asp:Button ID="btnview" runat="server" Text="View" Style="display: none" OnClick="btnview_Click"
                                            CausesValidation="false" />
                                        <asp:Button ID="btnPOPrint" runat="server" Text="Print"  Style="display: none" OnClick="btnIssuePrint_Click"
                                            CausesValidation="false" />

                                        <asp:Button ID="btnShoWProduction" runat="server" Text="PRODUCTION"  Style="display: none" OnClick="btnShoWProduction_Click"
                                            CausesValidation="false" /> 
                                        
                                        <asp:Button ID="btnShoWRequisition" runat="server" Text="REQUISITION"  Style="display: none" OnClick="btnShoWRequisition_Click"
                                            CausesValidation="false" />
                                    </div>


                                      <div id="divDataShow" runat="server"  class="Popup" align="center" style="overflow: scroll; height: 150px;">
                                        
                                        <asp:GridView ID="grvPopUp" runat="server" Width="450px" Height="10px" CssClass="zebra"
                                                AutoGenerateColumns="false" ShowFooter="false" AlternatingRowStyle-BackColor="AliceBlue"
                                                EmptyDataText="No Records Available">
                                             <Columns>
                                                 <asp:TemplateField HeaderText="PRODUCTION / REQUISITION NUMBER" ItemStyle-Width="200px" >
                                                      <ItemTemplate>
                                                           <asp:Label ID="lblNUMBER" runat="server" Text='<%# Bind("NUMBER") %>'
                                                                                value='<%# Eval("NUMBER") %>' Width="200px" ></asp:Label>
                                                           </ItemTemplate>
                                                      </asp:TemplateField>
                                             </Columns>
                                         </asp:GridView>
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
                function ValidateDate(sender, args) {
                    var dateString = document.getElementById(sender.controltovalidate).value;
                    var regex = /((0[0-9]|1[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$/;
                    if (regex.test(dateString)) {
                        var parts = dateString.split("/");
                        var dt = new Date(parts[1] + "/" + parts[0] + "/" + parts[2]);
                        args.IsValid = (dt.getDate() == parts[0] && dt.getMonth() + 1 == parts[1] && dt.getFullYear() == parts[2]);
                    } else {
                        args.IsValid = false;
                    }
                }
            </script>
            <script type="text/javascript">
                function CallServerMethod(oLink) {
                    var iRecordIndex = oLink.id.toString().replace("btnGridEdit_", "");
                    document.getElementById("<%=Hdn_Fld.ClientID %>").value = gvIssuegrid.Rows[iRecordIndex].Cells[1].Value;
                    document.getElementById("<%=btngrdedit.ClientID %>").click();
                }
                function CallFinalDeleteServerMethod(oLink) {
                    var iRecordIndexdelete = oLink.id.toString().replace("btnGridFinalDelete_", "");
                    document.getElementById("<%=Hdn_Fld.ClientID %>").value = gvIssuegrid.Rows[iRecordIndexdelete].Cells[1].Value;
                    document.getElementById("<%=btnfinalgrdDelete.ClientID %>").click();
                }

                function CallServerViewMethod(oLink) {
                    var iRecordIndex = oLink.id.toString().replace("btnGridEdit_", "");
                    document.getElementById("<%=Hdn_Fld.ClientID %>").value = gvIssuegrid.Rows[iRecordIndex].Cells[1].Value;
                    document.getElementById("<%=btnview.ClientID %>").click();
                }
                function ServerMethodforShowingProduction(oLink) {
                    var iRecordIndex = oLink.id.toString().replace("btnShoWProduction_", "");
                    document.getElementById("<%=Hdn_Fld.ClientID %>").value = gvIssuegrid.Rows[iRecordIndex].Cells[1].Value;
                    document.getElementById("<%=btnShoWProduction.ClientID %>").click();

                }


                function ServerMethodforShowingRequisition(oLink) {
                    var iRecordIndex = oLink.id.toString().replace("btnShoWRequisition_", "");
                    document.getElementById("<%=Hdn_Fld.ClientID %>").value = gvIssuegrid.Rows[iRecordIndex].Cells[1].Value;
                    document.getElementById("<%=btnShoWRequisition.ClientID %>").click();

                }


                function CallServerMethodPrint(oLink) {
                    var iRecordIndex = oLink.id.toString().replace("btnPOPrint_", "");
                    document.getElementById("<%=Hdn_Fld.ClientID %>").value = gvIssuegrid.Rows[iRecordIndex].Cells[1].Value;
                    document.getElementById("<%=btnPOPrint.ClientID %>").click();

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
                    gvpodetails.addFilterCriteria('PODATE', OboutGridFilterCriteria.Contains, searchValue);
                    gvpodetails.addFilterCriteria('PONO', OboutGridFilterCriteria.Contains, searchValue);
                    gvpodetails.addFilterCriteria('TPUNAME', OboutGridFilterCriteria.Contains, searchValue);
                    gvpodetails.executeFilter();
                    searchTimeout = null;
                    return false;
                }

                 function CheckAllheader(Checkbox) {
                    var GridVwHeaderCheckbox = document.getElementById("<%=gvrequistiondetails.ClientID %>");
                    var lbltotalcount = document.getElementById("<%=lbltotalcount.ClientID %>");
                     var totalcount = 0;
                     var rowCount = 0;
                      if (GridVwHeaderCheckbox.rows.length > 40) {
                             rowCount = 40+1;
                         }
                         else {
                             rowCount = GridVwHeaderCheckbox.rows.length;
                         }
                     if (Checkbox.checked) {
                        
                        for (i = 1; i < rowCount; i++) {
                            GridVwHeaderCheckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
                            /*GridVwHeaderCheckbox.rows[i].style.color = '#389638';
                            GridVwHeaderCheckbox.rows[i].cells[0].style.border = '1px solid #fff';*/

                            totalcount = totalcount + 1;
                        }
                    }
                    else {
                        for (i = 1; i < rowCount; i++) {
                            GridVwHeaderCheckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = false;
                            /*GridVwHeaderCheckbox.rows[i].style.color = '#4B555E';
                            GridVwHeaderCheckbox.rows[i].cells[0].style.border = '0px solid gray';*/
                        }
                    }
                    if (totalcount > 0) {
                        lbltotalcount.style.display = "";
                        lbltotalcount.innerHTML = 'You have selected : ' + totalcount + ' requisition.';
                    }
                    else {
                        lbltotalcount.style.display = "none";
                    }
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

                function ShowPanel(ctlbtn, ctladd, ctlshow) {
                    if (document.getElementById(ctlbtn)) {
                        document.getElementById(ctladd).style.display = '';
                        document.getElementById(ctlshow).style.display = 'none';

                        return false;
                    }
                }

                function ClosePanel(ctlbtn, ctladd, ctlshow) {
                    if (document.getElementById(ctlbtn)) {
                        document.getElementById(ctlshow).style.display = true;
                        document.getElementById(ctladd).style.display = false;
                        document.getElementById("ctl00_ContentPlaceHolder1_btnAdd").disabled = false;
                        return false;
                    }
                }

                function ShowHideInputs() {

                    if (document.all.InputTable.style.display == "none") {
                        document.all.InputTable.style.display = "inline";
                        document.all.HideInput.value = " Hide ";
                    }
                    Else
                    {
                        document.all.InputTable.style.display = "none";
                        document.all.HideInput.value = " Show ";
                    }

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