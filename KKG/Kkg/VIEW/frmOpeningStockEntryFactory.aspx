<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmOpeningStockEntryFactory.aspx.cs" Inherits="VIEW_frmOpeningStockEntryFactory" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register Namespace="OboutInc.Calendar2" Assembly="obout_Calendar2_Net" TagPrefix="cc1" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <style type="text/css">
        .customStyle1
        {
            position: Relative;
            left: 383px !important;
            /*top: 450px !important;
            background-color: #A8AEBD;*/
            border: Bold;
            color: #fff;
            background-color: #8ab8ed;
            text-shadow: 0 1px 0 rgba(0,0,0,.26);
            -moz-box-shadow: inset 0 4px 9px rgba(0,0,0,.24);
            -webkit-box-shadow: inset 0 4px 9px rgba(0,0,0,.24);
            box-shadow: inset 0 4px 9px rgba(0,0,0,.24);
        }
        
        .customStyleexp
        {
            position: Relative;
            left: 503px !important;
            /*top: 450px !important;
            background-color: #A8AEBD;*/
            border: Bold;
            color: #fff;
            background-color: #8ab8ed;
            text-shadow: 0 1px 0 rgba(0,0,0,.26);
            -moz-box-shadow: inset 0 4px 9px rgba(0,0,0,.24);
            -webkit-box-shadow: inset 0 4px 9px rgba(0,0,0,.24);
            box-shadow: inset 0 4px 9px rgba(0,0,0,.24);
        }
    </style>
    
    <script type="text/javascript">


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

        function CallDeleteServerMethod(oLink) {
            var iRecordIndexdelete = oLink.id.toString().replace("btnGridDelete_", "");
            document.getElementById("<%=hdndtAddStockDelete.ClientID %>").value = grdAddStock.Rows[iRecordIndexdelete].Cells[1].Value;
            document.getElementById("<%=btngriddelete.ClientID %>").click();
        }


        var searchTimeout = null;
        function FilterTextBox_KeyUp() {
            searchTimeout = window.setTimeout(performSearch, 500);
        }

        function performSearch() {
            var searchValue = document.getElementById('FilterTextBox').value;
            if (searchValue == FilterTextBox.WatermarkText) {
                searchValue = '';
            }
            gvOpenStock.addFilterCriteria('BRNAME', OboutGridFilterCriteria.Contains, searchValue);
            gvOpenStock.addFilterCriteria('BRAND', OboutGridFilterCriteria.Contains, searchValue);
            gvOpenStock.addFilterCriteria('CATAGORY', OboutGridFilterCriteria.Contains, searchValue);
            gvOpenStock.executeFilter();
            searchTimeout = null;
            return false;
        }

        function CallServerMethod(oLink) {
            var iRecordIndex = oLink.id.toString().replace("btnGridEdit_", "");
            document.getElementById("<%=hdnDepotID.ClientID %>").value = gvOpenStock.Rows[iRecordIndex].Cells[1].Value;
            document.getElementById("<%=hdnbrandid.ClientID %>").value = gvOpenStock.Rows[iRecordIndex].Cells[3].Value;
            document.getElementById("<%=hdncatagoryid.ClientID %>").value = gvOpenStock.Rows[iRecordIndex].Cells[4].Value;
            document.getElementById("<%=hdnlocationid.ClientID %>").value = gvOpenStock.Rows[iRecordIndex].Cells[9].Value;
            document.getElementById("<%=btngridedit.ClientID %>").click();

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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnOpenningDetails" />
        </Triggers>
        <ContentTemplate>
            <%--<div class="page_title">
           <span class="title_icon"><span class="computer_imac"></span></span>
              <h3>Openig Stock</h3>
        </div>--%>
            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active" id="trBtn" runat="server">
                                <span class="h_icon_he list_images"></span>
                                <h6>
                                    Opening Stock Details</h6>
                                <div class="btn_30_light" style="float: right;" id="btnaddhide" runat="server">
                                    <span class="icon add_co"></span>
                                    <asp:Button ID="btnAdd" runat="server" Text="Add New Record" CssClass="btn_link"
                                        CausesValidation="false" OnClick="btnAdd_Click" /></div> 
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input" style="padding-left: 10px">
                                                <fieldset>
                                                    <legend>Product Details</legend>
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td width="85" class="field_title" style="padding-bottom: 15px;">
                                                                <asp:Label ID="lblDeptName" Text="FACTORY" runat="server"></asp:Label>&nbsp;<span class="req">*</span>
                                                            </td>
                                                            <td width="240" class="field_input" style="padding-bottom: 10px;">
                                                                <asp:DropDownList ID="ddlDeptName" Width="230" runat="server" class="chosen-select"
                                                                    data-placeholder="Choose Dept Name" AppendDataBoundItems="True" ValidationGroup="ADD">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfv_ddlDeptName" runat="server" ControlToValidate="ddlDeptName"
                                                                    ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="ADD"
                                                                    ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td width="110" class="field_input">
                                                                <asp:TextBox ID="txtopeningdate" runat="server" Width="70"  placeholder="dd/mm/yyyy" MaxLength="10" Enabled="false"></asp:TextBox>
                                                                <asp:ImageButton ID="ImageButton1" runat="server"  ImageUrl="~/images/calendar.png" Width="24px" Height="24px" ImageAlign="AbsMiddle"  />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtopeningdate" PopupButtonID="ImageButton1" 
                                                                    runat="server" Format="dd/MM/yyyy"  Enabled="true" BehaviorID="CalendarExtender1" CssClass="cal_Theme1"/>
                                                                <span class="label_intro"><asp:Label ID="Label3" runat="server" Text="OPENING DATE"></asp:Label></span>
                                                            </td>
                                                            <td width="100" class="field_title" style="padding-bottom: 15px;">
                                                                <asp:Label ID="lblBrand" Text="PRIMARY ITEM" runat="server"></asp:Label>&nbsp;<span class="req">*</span>
                                                            </td>
                                                            <td width="218" class="field_input">
                                                                <asp:DropDownList ID="ddlBrand" Width="210" runat="server" class="chosen-select"
                                                                    data-placeholder="Choose Primary Item" ValidationGroup="ADD" AutoPostBack="True" OnSelectedIndexChanged="ddlBrand_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlBrand"
                                                                    ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="ADD"
                                                                    ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td width="75" class="field_title" style="padding-bottom: 15px;">
                                                                <asp:Label ID="lblCategory" Text="SUB ITEM" runat="server"></asp:Label>&nbsp;<span
                                                                    class="req">*</span>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:DropDownList ID="ddlCategory" Width="210" runat="server" class="chosen-select"
                                                                    data-placeholder="Choose Sub Item" ValidationGroup="ADD" AutoPostBack="True"
                                                                    OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorCategory" runat="server" ControlToValidate="ddlCategory"
                                                                    ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="ADD"
                                                                    ForeColor="Red" InitialValue="0">
                                                                </asp:RequiredFieldValidator><br />
                                                                <br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="field_title" width="85" style="padding-bottom: 15px;">
                                                                <asp:Label ID="lblProduct" Text="Product" runat="server"></asp:Label>&nbsp;<span class="req">*</span>
                                                            </td>
                                                            <td class="field_input" colspan="2">
                                                                <asp:DropDownList ID="ddlProduct" Width="350" class="chosen-select" runat="server"
                                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged"
                                                                    ValidationGroup="ADD">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfv_ddlProduct" runat="server" ControlToValidate="ddlProduct"
                                                                    ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="ADD"
                                                                    ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td colspan="2" style="padding-left:10px;">
                                                                <asp:Label ID="lblPackingSize" runat="server" Text="UOM"></asp:Label>&nbsp;<span
                                                                    class="req">*</span>&nbsp;&nbsp;&nbsp;&nbsp;
                                                                <asp:DropDownList ID="ddlPackingSize" Width="210" runat="server" class="chosen-select"
                                                                    ValidationGroup="ADD">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfv_ddlPackingSize" runat="server" ControlToValidate="ddlPackingSize"
                                                                    ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="ADD"
                                                                    ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td class="field_title" colspan="2" style="display:none" >
                                                                <asp:Label ID="lblBatchNO" runat="server" Text="Batch No"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                                                <asp:TextBox ID="txtBatchno" runat="server" Width="25%" ></asp:TextBox>                                                                
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="field_title" colspan="7">
                                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                    <tr>
                                                                        <td valign="top" style="padding-top: 11px;" width="85">
                                                                            <asp:Label ID="lblStock" Text="DETAILS" runat="server"></asp:Label>&nbsp;<span class="req">*</span>
                                                                        </td>
                                                                        <td valign="top" width="108">
                                                                            <asp:TextBox ID="txtStockqty" runat="server" CssClass="x_large" placeholder="Stockqty" ValidationGroup="ADD"
                                                                                Text="0" MaxLength="10" onkeypress="return isNumberKeyWithDot(event);"> </asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="rfv_txtStockqty" runat="server" Display="None" ErrorMessage="Stock qty is required!"
                                                                                ControlToValidate="txtStockqty" ValidateEmptyText="false" SetFocusOnError="true "
                                                                                ValidationGroup="ADD"> </asp:RequiredFieldValidator>
                                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                                                                TargetControlID="rfv_txtStockqty" PopupPosition="BottomLeft" HighlightCssClass="errormassage"
                                                                                WarningIconImageUrl="../images/050.png">
                                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                                            <span class="label_intro"><asp:Label ID="Label1" runat="server" Text="OPENING QTY"></asp:Label></span>
                                                                        </td>
                                                                        <td valign="top" width="108">
                                                                            <asp:TextBox ID="txtmrp" runat="server" CssClass="x_large" MaxLength="10" onkeypress="return isNumberKeyWithDot(event);"
                                                                                placeholder="MRP" Text="0" ValidationGroup="ADD"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                                                                                Font-Bold="true" ForeColor="Red" ControlToValidate="txtmrp" SetFocusOnError="true "
                                                                                ValidationGroup="ADD"> </asp:RequiredFieldValidator>
                                                                            <span class="label_intro"><asp:Label ID="Label2" runat="server" Text="OPENING MRP"></asp:Label></span>
                                                                        </td>
                                                                          <td valign="top" width="108">
                                                                            <asp:TextBox ID="txtRate" runat="server" CssClass="x_large" MaxLength="10" onkeypress="return isNumberKeyWithDot(event);"
                                                                                placeholder="Rate" Text="0" ValidationGroup="ADD"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"
                                                                                Font-Bold="true" ForeColor="Red" ControlToValidate="txtRate" SetFocusOnError="true "
                                                                                ValidationGroup="ADD"> </asp:RequiredFieldValidator>
                                                                            <span class="label_intro"><asp:Label ID="lblRate" runat="server" Text="RATE"></asp:Label></span>
                                                                        </td>
                                                                        <td width="120" style="display:none">
                                                                            <asp:TextBox ID="txtmfgdate" runat="server" Width="80"  placeholder="dd/mm/yyyy" OnTextChanged="txtmfgdate_TextChanged" AutoPostBack="true"
                                                                                MaxLength="10" ValidationGroup="ADD" onkeypress="return isNumberKeyWithslash(event);"></asp:TextBox>
                                                                            
                                                                            <%--<cc1:Calendar ID="Calendar1" runat="server" DatePickerMode="true"  TextBoxId="txtmfgdate" DatePickerImagePath="../images/calendar.png"  
                                                                            DatePickerSynchronize="true" AutoPostBack="true" CSSCalendar="customStyle1" DateFormat="dd/MM/yyyy" Columns="1" TextArrowLeft="&lt;" TextArrowRight="&gt;" Align="Under">
                                                                            </cc1:Calendar>--%>
                                                                            
                                                                            <asp:ImageButton ID="ImageButtonMFDate" runat="server" ImageUrl="~/images/calendar.png" Width="24px" Height="24px" ImageAlign="AbsMiddle"   />
                                               
                                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtenderMFDate" TargetControlID="txtmfgdate" PopupButtonID="ImageButtonMFDate" 
                                                                               runat="server" Format="dd/MM/yyyy"  Enabled="true" BehaviorID="CalendarExtenderMFDate" CssClass="cal_Theme1"/>
                                                                            <span class="label_intro"><asp:Label ID="Label4" runat="server" Text="MFG DATE"></asp:Label></span>
                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorMFDate" runat="server" ForeColor="Red"
                                                                                ControlToValidate="txtmfgdate" ValidationGroup="A" ErrorMessage="Invalid" 
                                                                                ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$"></asp:RegularExpressionValidator>
                                                                          <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidatorInvoiceDate" runat="server"
                                                                                ControlToValidate="txtmfgdate" ValidateEmptyText="false" SetFocusOnError="true"
                                                                                ErrorMessage="Required!" ValidationGroup="ADD" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                                        </td>
                                                                        <td width="120" style="display:none">
                                                                            <asp:TextBox ID="txtexpdate" runat="server" Width="80" placeholder="dd/mm/yyyy"
                                                                                MaxLength="10" ValidationGroup="ADD" onkeypress="return isNumberKeyWithslash(event);"></asp:TextBox>

                                                                            <%--<cc1:Calendar ID="Calendar2" runat="server" DatePickerMode="true"  TextBoxId="txtexpdate" DatePickerImagePath="../images/calendar.png" 
                                                                            DatePickerSynchronize="true" AutoPostBack="false" CSSCalendar="customStyleexp" DateFormat="dd/MM/yyyy" Columns="1" TextArrowLeft="&lt;" TextArrowRight="&gt;" Align="Under">
                                                                            </cc1:Calendar>--%>

                                                                            <asp:ImageButton ID="ImageButtonExprDate" runat="server" ImageUrl="~/images/calendar.png" Width="24px" Height="24px" ImageAlign="AbsMiddle"   />
                                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtenderExprDate" TargetControlID="txtexpdate" PopupButtonID="ImageButtonExprDate" 
                                                                               runat="server" Format="dd/MM/yyyy"  Enabled="true" BehaviorID="CalendarExtenderExprDate" CssClass="cal_Theme1"/>
                                                                            <span class="label_intro"><asp:Label ID="Label5" runat="server" Text="EXP DATE"></asp:Label></span>
                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorExprDate" runat="server" ForeColor="Red"
                                                                                ControlToValidate="txtexpdate" ValidationGroup="A" ErrorMessage="Invalid" 
                                                                                ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$"></asp:RegularExpressionValidator>

                                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                                                                ControlToValidate="txtexpdate" ValidateEmptyText="false" SetFocusOnError="true"
                                                                                ErrorMessage="Required!" ValidationGroup="ADD" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                                        </td>
                                                                        <td width="115" valign="top" style="padding-top: 11px">
                                                                            <asp:Label ID="lblstorelocation" Text="Store Loaction " runat="server"></asp:Label><span
                                                                                class="req">*</span>
                                                                        </td>
                                                                        <td width="190" valign="top">
                                                                            <asp:DropDownList ID="ddlstorelocation" runat="server" AppendDataBoundItems="true"
                                                                                Width="170" class="chosen-select" ValidationGroup="ADD" data-placeholder="-- Store Loacation --">
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="* Required!"
                                                                                ValidationGroup="ADD" Font-Bold="true" ForeColor="Red" ControlToValidate="ddlstorelocation"
                                                                                SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>
                                                                        </td>
                                                                        <td valign="top">
                                                                        <div class="btn_24_blue">
                                                                            <span class="icon blue_block"></span>
                                                                            <asp:Button ID="btnAddStock" runat="server" Text="ADD" CssClass="btn_link" ValidationGroup="ADD"
                                                                                OnClick="btnAddStock_Click" />
                                                                        </div>
                                                                            <asp:HiddenField ID="Hdnassementpercentage" runat="server" />
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
                                    <div class="gridcontent-inner">
                                        <fieldset>
                                            <legend>Open Stock Details</legend>
                                            <cc1:Grid ID="grdAddStock" runat="server" Serialize="true" AutoGenerateColumns="false"
                                                FolderStyle="../GridStyles/premiere_blue" AllowSorting="false" ShowColumnsFooter="false"
                                                AllowPageSizeSelection="false" AllowPaging="false" PageSize="900" AllowAddingRecords="false">
                                                <FilteringSettings InitialState="Visible" />
                                                <Columns>
                                                    <cc1:Column HeaderText="Sl.No." AllowEdit="false" AllowDelete="false" Width="60">
                                                        <TemplateSettings TemplateId="tplNumberingSlnoSchemeProduct" />
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column2" DataField="GUID" HeaderText="GUID" runat="server" Width="60"
                                                        Visible="false" />
                                                    <cc1:Column ID="Column3" DataField="BRID" HeaderText="BSID" runat="server" Width="80"
                                                        Visible="false" />
                                                    <cc1:Column ID="Column4" DataField="BRNAME" HeaderText=" DEPOT NAME" runat="server"
                                                        Width="150" Wrap="true" Visible="false" />
                                                    <cc1:Column ID="Column9" DataField="DIVISIONID" HeaderText="DIVISIONID" runat="server"
                                                        Width="80" Visible="false" />
                                                    <cc1:Column ID="Column10" DataField="DIVISIONNAME" HeaderText="PRIMARY ITEM" runat="server"
                                                        Width="150" Visible="false">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column11" DataField="CATEGORYID" HeaderText="CATEGORYID" runat="server"
                                                        Width="80" Visible="false">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column13" DataField="CATEGORYNAME" HeaderText="SUB ITEM" runat="server"
                                                        Width="150" Wrap="true" Visible="false">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column18" DataField="PRODUCTID" HeaderText="PRODUCTID" runat="server"
                                                        Width="80" Visible="false" />
                                                    <cc1:Column ID="Column21" DataField="PRODUCTNAME" HeaderText="PRODUCT" runat="server"
                                                        Width="350" Wrap="true" />
                                                    <cc1:Column ID="Column22" DataField="BATCHNO" HeaderText="BATCH NO" runat="server"
                                                        Width="130" Wrap="true" />
                                                    <cc1:Column ID="Column23" DataField="STOCKQTY" HeaderText="STOCK QTY" runat="server"
                                                        Width="100" Wrap="true" />
                                                    <cc1:Column ID="Column1" DataField="UOMID" HeaderText="PACKINGSIZEID" runat="server"
                                                        Width="80" Visible="false" />
                                                    <cc1:Column ID="Column5" DataField="PACKSIZENAME" HeaderText="PACK SIZE" runat="server"
                                                        Width="150" Wrap="true" />
                                                    <cc1:Column ID="Column16" DataField="MRP" HeaderText="MRP" runat="server" Width="90"
                                                        Wrap="true" />
                                                    <cc1:Column ID="Column27" DataField="DEPOTRATE" HeaderText="RATE" runat="server"
                                                        Width="90" Wrap="true" />
                                                    <cc1:Column ID="Column17" DataField="MFGDATE" HeaderText="MFG DATE" runat="server"
                                                        Width="100" Wrap="true" />
                                                    <cc1:Column ID="Column20" DataField="EXPDATE" HeaderText="EXP DATE" runat="server"
                                                        Width="100" Wrap="true" />
                                                    <cc1:Column ID="Column25" DataField="NETWEIGHT" HeaderText="NET WEIGHT" runat="server"
                                                        Width="110" Wrap="true" />
                                                    <cc1:Column ID="Column26" DataField="GROSSWEIGHT" HeaderText="GROSS WEIGHT" runat="server"
                                                        Width="130" Wrap="true" />
                                                    <cc1:Column ID="Column24" DataField="ASSESMENTPERCENTAGE" HeaderText="ASSESMENT %"
                                                        runat="server" Width="110" Wrap="true" />
                                                    <cc1:Column DataField="STORELOCATIONNAME" HeaderText="STORE LOCATION" runat="server"
                                                        Width="160" Wrap="true" />
                                                    <cc1:Column DataField="STORELOCATIONID" HeaderText="STORELOCATIONID" runat="server"
                                                        Visible="false" Width="180" Wrap="true" />
                                                    <cc1:Column ID="Column32" DataField="TAG" HeaderText="TAG" runat="server"
                                                        Visible="false" Width="80" Wrap="true"/>
                                                    <cc1:Column HeaderText="DELETE" AllowEdit="false" AllowDelete="true" Width="60">
                                                        <TemplateSettings TemplateId="deleteBtnTemplate" />
                                                    </cc1:Column>
                                                </Columns>
                                                <FilteringSettings MatchingType="AnyFilter" />
                                                <Templates>
                                                    <cc1:GridTemplate runat="server" ID="tplNumberingSlnoSchemeProduct">
                                                        <Template>
                                                            <asp:Label ID="lblslnoSchemeProduct" runat="server" Text='<%#Container.PageRecordIndex+1 %>'></asp:Label>
                                                        </Template>
                                                    </cc1:GridTemplate>
                                                    <cc1:GridTemplate runat="server" ID="deleteBtnTemplate">
                                                        <Template>
                                                            <a href="javascript: //" class="action-icons c-delete" id="btnGridDelete_<%# Container.PageRecordIndex %>"
                                                                onclick="CallDeleteServerMethod(this)"></a>
                                                        </Template>
                                                    </cc1:GridTemplate>
                                                </Templates>
                                                <ScrollingSettings ScrollWidth="100%" ScrollHeight="300" NumberOfFixedColumns="10" />
                                            </cc1:Grid>
                                            <asp:Button ID="btngriddelete" runat="server" Text="grddelete" Style="display: none"
                                                OnClick="btngriddelete_Click" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete this record?');" />
                                            <asp:HiddenField ID="hdndtAddStockDelete" runat="server" />
                                        </fieldset>
                                    </div>
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input" style="padding-left: 10px">
                                                <div class="btn_24_blue">
                                                    <span class="icon disk_co"></span>
                                                    <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="btn_link" ValidationGroup="Save"
                                                        OnClick="btnSubmit_Click" />
                                                </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_blue">
                                                    <span class="icon cross_octagon_co"></span>
                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn_link" CausesValidation="false"
                                                        OnClick="btnCancel_Click" />
                                                </div>
                                                <asp:HiddenField runat="server" ID="hdnDepotID" />
                                                <asp:HiddenField runat="server" ID="hdnbrandid" />
                                                <asp:HiddenField runat="server" ID="hdncatagoryid" />
                                                <asp:HiddenField runat="server" ID="hdnlocationid" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlDisplay" runat="server">

                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                        <tr>
                                     <div class="btn_24_blue" style="float: left;" id="btnOpenning" runat="server">
                                <span class="icon link_sl"></span>
                                <asp:Button ID="btnOpenningDetails" runat="server" Text="Openning Product Info" CssClass="btn_link"
                                CausesValidation="false" OnClick="btnOpenningDetails_Click" /></div> 
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
                                    <div class="gridcontent" style="padding-left: 8px;">
                                        <cc1:Grid ID="gvOpenStock" runat="server" CallbackMode="true" Serialize="true" FolderStyle="../GridStyles/premiere_blue"
                                            AutoGenerateColumns="false" AllowAddingRecords="false" AllowFiltering="true"
                                            AllowSorting="true" AllowPageSizeSelection="true" PageSize="500" AllowPaging="false">
                                            <FilteringSettings InitialState="Visible" />
                                            <Columns>
                                                <cc1:Column ID="Column12" DataField="Sl" HeaderText="Sl" runat="server" Width="70"
                                                    AllowFilter="false">
                                                    <TemplateSettings TemplateId="slnoTemplateIN" />
                                                </cc1:Column>
                                                <cc1:Column ID="Column19" DataField="BRID" HeaderText="BRID" runat="server" Width="1"
                                                    Visible="false" />
                                                <cc1:Column ID="Column8" DataField="BRNAME" HeaderText="DEPOT NAME" runat="server"
                                                    Width="1" Wrap="true" Visible="false">
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
                                                <cc1:Column ID="Column14" DataField="DIVID" HeaderText="PRIMARY ITEM" runat="server" Width="1"
                                                    Visible="false" >
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
                                                <cc1:Column ID="Column15" DataField="CATID" HeaderText="CATAGORY1" runat="server"
                                                    Width="1" Visible="false" />
                                                <cc1:Column ID="Column6" DataField="BRAND" HeaderText="PRIMARY ITEM" runat="server" Width="200"
                                                    Wrap="true" />
                                                <cc1:Column ID="Column7" DataField="CATAGORY" HeaderText="SUB ITEM" runat="server"
                                                    Width="200" Wrap="true" >
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
                                                <cc1:Column ID="Column28" DataField="TOTAL PRODUCT" HeaderText="TOTAL PRODUCTS (Pcs)" runat="server"
                                                    Width="180" Wrap="true">
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
                                                
                                                <cc1:Column ID="Column29" DataField="STORELOCATIONNAME" HeaderText="STORELOCATION" runat="server"
                                                    Width="200" Wrap="true">
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
                                                <cc1:Column ID="Column30" DataField="STORELOCATIONID" HeaderText="STORELOCATIONID" runat="server"
                                                    Width="1px" Wrap="true" Visible="false" />
                                                <cc1:Column ID="Column31" DataField="OPENINGENTRYDATE" HeaderText="OPENING DATE" runat="server"
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
                                                <cc1:Column ID="Column92" AllowEdit="true" AllowDelete="true" HeaderText="EDIT" runat="server" Wrap="true"
                                                    Width="100%">
                                                    <TemplateSettings TemplateId="editBtnTemplate" />
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
                                                <cc1:GridTemplate runat="server" ID="slnoTemplateIN">
                                                    <Template>
                                                        <asp:Label ID="lblslnoin" runat="server" Text='<%#Container.PageRecordIndex+1 %>'></asp:Label>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <ScrollingSettings ScrollWidth="100%" ScrollHeight="320" />
                                        </cc1:Grid>
                                        <asp:Button ID="btngridedit" runat="server" Text="grdedit" Style="display: none"
                                            CausesValidation="false" OnClick="btngridedit_Click" />
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                    <cc1:MessageBox ID="MessageBox1" runat="server" />
                </div>
            </div>
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

                function isNumberKeyWithDotMinus(evt) {
                    var charCode = (evt.which) ? evt.which : event.keyCode;
                    if ((charCode < 48 || charCode > 57) && (charCode != 8) && (charCode != 46) && (charCode != 45))
                        return false;

                    return true;
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>