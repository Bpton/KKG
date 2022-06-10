<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true"
    CodeFile="frmPurchaseReturn_MM.aspx.cs" Inherits="VIEW_frmPurchaseReturn_MM" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
        var searchTimeout = null;
        function FilterTextBox_KeyUp() {
            searchTimeout = window.setTimeout(performSearch, 500);
        }

        function performSearch() {
            var searchValue = document.getElementById('FilterTextBox').value;
            if (searchValue == FilterTextBox.WatermarkText) {
                searchValue = '';
            }
            grdDespatchHeader.addFilterCriteria('SALERETURNNO', OboutGridFilterCriteria.Contains, searchValue);
            grdDespatchHeader.addFilterCriteria('SALERETURNDATE', OboutGridFilterCriteria.Contains, searchValue);
            grdDespatchHeader.addFilterCriteria('VENDORNAME', OboutGridFilterCriteria.Contains, searchValue);
            grdDespatchHeader.executeFilter();
            searchTimeout = null;
            return false;
        }

        function OnBeforeInvoiceDelete(record) {
            record.Error = '';
            document.getElementById("<%=hdnDespatchID.ClientID %>").value = record.SALEINVOICEID;
            if (confirm("Are you sure you want to delete? "))
                return true;
            else
                return false;
        }
        function OnDeleteInvoiceDetails(record) {
            alert(record.Error);
        }

        function CallServerMethod(oLink) {
            var iRecordIndex = oLink.id.toString().replace("btnGridEdit_", "");
            document.getElementById("<%=hdnDespatchID.ClientID %>").value = grdDespatchHeader.Rows[iRecordIndex].Cells[1].Value;
            document.getElementById("<%=btngrdedit.ClientID %>").click();

        }
        function CallServerMethodPrint(oLink) {
            var iRecordIndex = oLink.id.toString().replace("btnPrint_", "");
            document.getElementById("<%=hdnDespatchID.ClientID %>").value = grdDespatchHeader.Rows[iRecordIndex].Cells[1].Value;
            document.getElementById("<%=Hdn_Print.ClientID %>").value = grdDespatchHeader.Rows[iRecordIndex].Cells[4].Value;
            document.getElementById("<%=btnPrint.ClientID %>").click();

        }
    </script>

    <script language="javascript" type="text/javascript">
        function MakeStaticHeader(gridId, height, width, headerHeight, isFooter) {
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

    <asp:UpdateProgress ID="UpdateProgress" runat="server">
        <ProgressTemplate>
            <asp:Image ID="Image1" ImageUrl="../images/loading123.gif" AlternateText="Processing"
                runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
        PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>

            <div id="content">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active" id="trBtn" runat="server">
                                <span class="h_icon_he list_images"></span>
                                <h6>PURCHASE RETURN DETAILS</h6>
                                <div class="btn_30_light" style="float: right;" id="btnaddhide" runat="server">
                                    <span class="icon add_co"></span>
                                    <asp:Button ID="btnAdd" runat="server" Text="Add New Record" CssClass="btn_link"
                                        CausesValidation="false" OnClick="btnAdd_Click" />
                                </div>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input">
                                                <fieldset>
                                                    <legend>PURCHASE RETURN DETAILS</legend>
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                        <tr runat="server" id="trAutoInvoiceNo">
                                                            <td class="field_title">
                                                                <asp:Label ID="lblStockRcvdNo" Text="Purchase Return No." runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input" colspan="2">
                                                                <asp:TextBox ID="txtSaleInvoiceNo" runat="server" CssClass="large" placeholder="Auto Generate No."
                                                                    Enabled="false" Font-Bold="true"> </asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="field_title">
                                                                <asp:Label ID="Label36" Text="Return Date" runat="server"></asp:Label>&nbsp;<span
                                                                    class="req">*</span>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtInvoiceDate" runat="server" Enabled="false" Width="80" placeholder="dd/MM/yyyy"
                                                                    MaxLength="10" ValidationGroup="Save"></asp:TextBox>
                                                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/calendar.png"
                                                                    Width="24px" Height="24px" ImageAlign="AbsMiddle" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" TargetControlID="txtInvoiceDate"
                                                                    PopupButtonID="ImageButton1" runat="server" Format="dd/MM/yyyy" Enabled="true"
                                                                    BehaviorID="CalendarExtenderDespatchDate" CssClass="cal_Theme1" />
                                                            </td>
                                                            <td class="field_title" id="TdlblReturnType" runat="server">
                                                                <asp:Label ID="lblReturnType" Text="Return Type" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input" id="TdddlReturnType" runat="server">
                                                                <asp:DropDownList ID="ddlReturnType" runat="server" class="chosen-select" data-placeholder="Select Type"
                                                                    AppendDataBoundItems="True" Width="125px" ValidationGroup="A" AutoPostBack="true"
                                                                    OnSelectedIndexChanged="ddlReturnType_SelectedIndexChanged">
                                                                    <asp:ListItem Value="N" Text="Normal"></asp:ListItem>
                                                                    <asp:ListItem Value="G" Text="QC Rejection"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>

                                                            <td class="field_title" id="TdlblGrnRejectionNo" runat="server">
                                                                <asp:Label ID="lblGrnRejectionNo" Text="Grn Rejection No" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input" id="TdddlGrnRejectionNo" runat="server">
                                                                <asp:DropDownList ID="ddlGrnRejectionNo" runat="server" class="chosen-select"
                                                                    AppendDataBoundItems="True" Width="135px" ValidationGroup="A" AutoPostBack="true"
                                                                    OnSelectedIndexChanged="ddlGrnRejectionNo_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="field_title">
                                                                <asp:Label ID="Label5" Text="Supplied Item" runat="server"></asp:Label>&nbsp;<span class="req">*</span>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:DropDownList ID="ddlSuppliedItem" runat="server" class="chosen-select" data-placeholder="Select Supplied Item"
                                                                    AppendDataBoundItems="True" Width="130px" ValidationGroup="A" AutoPostBack="true"
                                                                    OnSelectedIndexChanged="ddlSuppliedItem_SelectedIndexChanged">
                                                                </asp:DropDownList>

                                                            </td>
                                                            <td valign="top" style="padding-top: 15px;" class="field_title">
                                                                <asp:Label ID="lblTPU" Text="Vendor" runat="server"></asp:Label>&nbsp;<span
                                                                    class="req">*</span>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:DropDownList ID="ddlVendor" runat="server" class="chosen-select" data-placeholder="Select Vendor"
                                                                    AppendDataBoundItems="True" Width="195px" ValidationGroup="A" AutoPostBack="true"
                                                                    OnSelectedIndexChanged="ddlVendor_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td class="field_title" id="tdlblledger" runat="server">
                                                                <asp:Label ID="lblledger" Text="Ledger" runat="server"></asp:Label>&nbsp;<span class="req">*</span>
                                                            </td>
                                                            <td class="field_input" id="tdddlledger" runat="server">
                                                                <asp:DropDownList ID="ddlledger" ValidationGroup="approval" Width="240" runat="server"
                                                                    class="chosen-select">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="Reqrdddlledger" runat="server" ControlToValidate="ddlledger"
                                                                    InitialValue="0" SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="approval"
                                                                    ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td valign="top" style="padding-top: 15px;" class="field_title">
                                                                <asp:Label ID="Label14" Text="Depot" runat="server" Visible="false"></asp:Label>
                                                            </td>
                                                            <td class="field_input" valign="top" style="padding-top: 8px;">
                                                                <asp:DropDownList ID="ddlDepot" Width="160" runat="server" Enabled="false" class="chosen-select"
                                                                    data-placeholder="Choose Depot" AppendDataBoundItems="True" ValidationGroup="Save" Visible="false">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>&nbsp;
                                                            </td>
                                                            <td>&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr id="divsearch" runat="server">
                                                            <td width="110" class="field_title">
                                                                <asp:Label ID="Label31" Text="From Date" runat="server"></asp:Label>&nbsp;<span class="req">*</span><br />
                                                                <br />
                                                            </td>
                                                            <td width="165" class="field_input">
                                                                <asp:TextBox ID="txtinvoicefromdate" runat="server" Enabled="false" Width="80" placeholder="dd/MM/yyyy"
                                                                    MaxLength="10" ValidationGroup="Save"></asp:TextBox>
                                                                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/calendar.png"
                                                                    Width="24px" Height="24px" ImageAlign="AbsMiddle" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="ImageButton2"
                                                                    runat="server" TargetControlID="txtinvoicefromdate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </td>
                                                            <td class="field_title">
                                                                <asp:Label ID="Label32" Text="To Date" runat="server"></asp:Label>&nbsp;<span class="req">*</span><br />
                                                                <br />
                                                            </td>
                                                            <td width="165" class="field_input">
                                                                <asp:TextBox ID="txtinvoicetodate" runat="server" Enabled="false" Width="80" placeholder="dd/MM/yyyy"
                                                                    MaxLength="10" ValidationGroup="Save"></asp:TextBox>
                                                                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/images/calendar.png"
                                                                    Width="24px" Height="24px" ImageAlign="AbsMiddle" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImageButton3"
                                                                    runat="server" TargetControlID="txtinvoicetodate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </td>
                                                            <td>
                                                                <div class="btn_24_blue" style="margin-bottom: 14px;">
                                                                    <span class="icon find_co"></span>
                                                                    <asp:Button ID="btnSearchRetailerInvoice" runat="server" Text="Search" CssClass="btn_link"
                                                                        ValidationGroup="Search" OnClick="btnSearchRetailerInvoice_Click" />
                                                                </div>
                                                            </td>
                                                            <td>&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr id="trinvoiceno" runat="server">
                                                            <td class="field_title">
                                                                <asp:Label ID="Label34" Text="grn No" runat="server"></asp:Label>&nbsp;<span
                                                                    class="req">*</span>
                                                            </td>
                                                            <td class="field_input" colspan="3">
                                                                <asp:DropDownList ID="ddlretailerinvoice" runat="server" Width="600px" Height="24px"
                                                                    CssClass="common-select" AutoPostBack="true" AppendDataBoundItems="True" Style="font-family: 'Courier New', Courier, monospace"
                                                                    OnSelectedIndexChanged="ddlretailerinvoice_SelectedIndexChanged" ValidationGroup="Save">
                                                                </asp:DropDownList>
                                                             
                                                            </td>
                                                            <td class="field_title">&nbsp;
                                                            </td>
                                                            <td class="field_input">&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="field_title">
                                                                <asp:Label ID="lblinvno" Text="Invoice No" runat="server"></asp:Label>&nbsp;
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtinvno" runat="server" Enabled="false" Width="100" ValidationGroup="Save"></asp:TextBox>
                                                            </td>
                                                            <td class="field_title">
                                                                <asp:Label ID="lblinvdt" Text="Invoice Date" runat="server"></asp:Label>&nbsp;
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtinvdt" runat="server" Enabled="false" Width="80" placeholder="dd/MM/yyyy"
                                                                    MaxLength="10" ValidationGroup="Save"></asp:TextBox>&nbsp;
                                                            </td>

                                                            <td class="field_title">
                                                                <asp:Label ID="lbllrgrno" Text="LR/GR NO" runat="server"></asp:Label>&nbsp;
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtlrgrno" runat="server" Width="80"></asp:TextBox>
                                                            </td>
                                                            <td class="field_title">
                                                                <asp:Label ID="lbllrgrdt" Text="LR/GR DATE" runat="server"></asp:Label>&nbsp;
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtlrgrdt" runat="server" Width="80" placeholder="dd/MM/yyyy"
                                                                    MaxLength="10"></asp:TextBox>
                                                                <asp:ImageButton ID="Imglrgrdt" runat="server" ImageUrl="~/images/calendar.png"
                                                                    Width="24px" Height="24px" ImageAlign="AbsMiddle" />
                                                                <ajaxToolkit:CalendarExtender ID="Callrgrdt" PopupButtonID="Imglrgrdt"
                                                                    runat="server" TargetControlID="txtlrgrdt" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                                </ajaxToolkit:CalendarExtender>

                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td class="field_title">
                                                                <asp:Label ID="lbltransporter" Text="TRANSPORTER" runat="server"></asp:Label>&nbsp;<span class="req">*</span>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:DropDownList ID="ddltransporter" runat="server" Width="200px" Height="24px" class="chosen-select"
                                                                    AppendDataBoundItems="True" ValidationGroup="Save">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="ReqTransporter" runat="server" ControlToValidate="ddltransporter"
                                                                    ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="Save"
                                                                    ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td class="field_title">
                                                                <asp:Label ID="lblvehicalno" Text="VEHICALNO" runat="server"></asp:Label>&nbsp;
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtvehicalno" runat="server" Width="80"></asp:TextBox>
                                                            </td>
                                                        </tr>

                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>

                                    </table>
                                    <div class="gridcontent-inner" id="divproduct" runat="server">
                                        <fieldset>
                                            <legend>Invoice Details</legend>
                                            <div style="overflow: hidden; width: 100%;" id="DivHeaderRow">
                                            </div>
                                            <div style="overflow: scroll; margin-bottom: 8px; margin-right: 6px;" onscroll="OnScrollDiv(this)" id="DivMainContent">

                                                <asp:GridView ID="grdinvoicedetails" runat="server" Width="100%" CssClass="zebra"
                                                    AutoGenerateColumns="true" EmptyDataText="No Records Available"
                                                    ShowFooter="true">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SL NO." ItemStyle-Width="10" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="RETURN QTY" ItemStyle-Width="30" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtreturnqty" runat="server" onkeypress="return isNumberKeyWithDot(event);" Width="45"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div id="DivFooterRow" style="overflow: hidden">
                                            </div>

                                        </fieldset>
                                    </div>
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr id="trbtnreturnadd" runat="server">
                                            <td align="right" style="padding-top: 10px; padding-right: 27px;">&nbsp;
                                            
                                            </td>
                                            <td align="right" style="padding-top: 10px; padding-right: 27px;">&nbsp;
                                            </td>
                                            <td align="right" style="padding-top: 10px; padding-right: 27px;">
                                                <asp:RadioButton ID="rdbTax" runat="server" Text="With&nbsp;Tax" Checked="true" GroupName="TAX" AutoPostBack="true" OnCheckedChanged="rdbTax_CheckedChanged" Visible="false" />&nbsp;&nbsp;&nbsp;
                                                <asp:RadioButton ID="rdbNoTax" runat="server" Text="Without&nbsp;Tax" GroupName="TAX" AutoPostBack="true" OnCheckedChanged="rdbNoTax_CheckedChanged" Visible="false" />&nbsp;&nbsp;
                                                <div class="btn_24_blue">
                                                    <span class="icon add_co"></span>
                                                    <asp:Button ID="btnreturnadd" runat="server" CssClass="btn_link" Text="ADD" CausesValidation="false"
                                                        OnClick="btnreturnadd_click" />
                                                </div>
                                            </td>

                                        </tr>
                                    </table>
                                    <div class="gridcontent-inner">
                                        <fieldset>
                                            <legend>Return Product</legend>

                                            <div class="zebra" style="margin-bottom: 8px;">
                                                <asp:GridView ID="grdAddDespatch" runat="server" Width="100%" ShowFooter="true" utoGenerateColumns="true"
                                                    EmptyDataText="No Records Available">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="DELETE">
                                                            <ItemTemplate>
                                                                <nav style="height: 0px; z-index: 2; position: relative; text-align: center; padding-left: 10px;"><span class="badge green"><%# Container.DataItemIndex + 1 %></span></nav>
                                                                <asp:ImageButton ID="btn_TempDelete" runat="server" CssClass="action-icons c-delete"
                                                                    OnClientClick="return confirm('Are you sure you want to Delete?')" ToolTip="Delete"
                                                                    OnClick="btn_TempDelete_Click" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:HiddenField ID="hdndtDespatchDelete" runat="server" />
                                                <asp:HiddenField ID="hdndtPOIDDelete" runat="server" />
                                                <asp:HiddenField ID="hdndtPRODUCTIDDelete" runat="server" />
                                                <asp:HiddenField ID="hdndtBATCHDelete" runat="server" />
                                                <asp:HiddenField ID="hdn_bsid" runat="server" />
                                                <asp:HiddenField ID="hdn_bsname" runat="server" />
                                            </div>
                                        </fieldset>
                                    </div>

                                    <div class="gridcontent-inner" style="display: none;">
                                        <fieldset>
                                            <legend>Free Return Product</legend>
                                            <div class="zebra" style="margin-bottom: 8px;">
                                                <asp:GridView ID="grvFreeProduct" runat="server" Width="100%" ShowFooter="true"
                                                    AutoGenerateColumns="true"
                                                    EmptyDataText="No Records Available">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="DELETE">
                                                            <ItemTemplate>
                                                                <nav style="height: 0px; z-index: 2; position: relative; text-align: center; padding-left: 10px;"><span class="badge green"><%# Container.DataItemIndex + 1 %></span></nav>
                                                                <asp:ImageButton ID="btn_TempFreeDelete" runat="server" CssClass="action-icons c-delete"
                                                                    OnClientClick="return confirm('Are you sure you want to Delete?')" ToolTip="Delete" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </fieldset>
                                    </div>

                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input" style="padding-left: 10px">
                                                <fieldset>
                                                    <legend>Item Wise Amount</legend>
                                                    <table>
                                                        <tr>
                                                            <td width="100" class="innerfield_title" style="display: none;">
                                                                <asp:Label ID="Label311" Text="Tot.SCH.AMT." runat="server"></asp:Label>
                                                            </td>
                                                            <td width="125" style="display: none;">
                                                                <asp:TextBox runat="server" ID="txtTotMRP" CssClass="x_large" placeholder="Tot.SCH.Amt."
                                                                    Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td width="90" class="innerfield_title">
                                                                <asp:Label ID="Label21" Text="Ret.Amt." runat="server"></asp:Label>
                                                            </td>
                                                            <td width="125">
                                                                <asp:TextBox runat="server" ID="txtAmount" CssClass="x_large" placeholder="Ret Amt"
                                                                    Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td width="90" class="innerfield_title">
                                                                <asp:Label ID="Label22" Text="Tax&nbsp;Amt." runat="server" Visible="true"></asp:Label>
                                                            </td>
                                                            <td width="125">
                                                                <asp:TextBox runat="server" ID="txtTotTax" CssClass="x_large" placeholder="Tax&nbsp;Amt."
                                                                    Enabled="false" Visible="true"></asp:TextBox>
                                                            </td>
                                                            <td width="100" class="innerfield_title">
                                                                <asp:Label ID="Label23" Text="(Ret.&nbsp;+&nbsp;Tax) Amt." runat="server" Visible="true"></asp:Label>
                                                            </td>
                                                            <td width="130">
                                                                <asp:TextBox runat="server" ID="txtNetAmt" CssClass="x_large" placeholder="(Ret.&nbsp;+&nbsp;Tax) Amt."
                                                                    Enabled="false" Visible="true"></asp:TextBox>
                                                            </td>
                                                            <td width="90" class="innerfield_title" style="display: none;">
                                                                <asp:Label ID="Label18" Text="Tot.&nbsp;Special&nbsp;Disc." runat="server" Visible="true"></asp:Label>
                                                            </td>
                                                            <td style="display: none;">
                                                                <asp:TextBox runat="server" ID="txtTotDisc" CssClass="x_large" placeholder="Tot.&nbsp;Special&nbsp;Disc."
                                                                    Enabled="false" Visible="true"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td class="field_input" style="padding-left: 10px">
                                                <fieldset>
                                                    <legend>Gross&nbsp;Amount&nbsp;Details</legend>
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td width="100" class="innerfield_title">
                                                                <asp:Label ID="Label24" Text="Gross Amt." runat="server"></asp:Label>
                                                            </td>
                                                            <td width="115">
                                                                <asp:TextBox runat="server" ID="txtTotalGross" CssClass="x-large" placeholder="Gross Amt."
                                                                    Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td width="15">&nbsp;
                                                            </td>
                                                            <td width="85" class="innerfield_title">
                                                                <asp:Label ID="Label27" Text="R/O" runat="server"></asp:Label>
                                                            </td>
                                                            <td width="115">
                                                                <asp:TextBox runat="server" ID="txtRoundoff" CssClass="x-large" placeholder="R/O"
                                                                    Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td width="15">&nbsp;
                                                            </td>
                                                            <td width="85" class="innerfield_title">
                                                                <asp:Label ID="Label33" Text="Net Amt." runat="server"></asp:Label>
                                                            </td>
                                                            <td width="125">
                                                                <asp:TextBox runat="server" ID="txtFinalAmt" CssClass="x_large" placeholder="Net Amt."
                                                                    Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td width="15">&nbsp;
                                                            </td>
                                                            <td width="75" class="innerfield_title" style="display: none">
                                                                <asp:Label ID="Label1" Text="Tot.PCS." runat="server"></asp:Label>
                                                            </td>
                                                            <td width="128" style="display: none">
                                                                <asp:TextBox runat="server" ID="txtTotPCS" CssClass="x_large" placeholder="Tot.PCS."
                                                                    Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td>&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr style="display: none;">
                                                            <td width="98" class="innerfield_title">&nbsp;
                                                            </td>
                                                            <td width="150">&nbsp;
                                                            </td>
                                                            <td width="70" class="innerfield_title">
                                                                <asp:Label ID="Label25" Text="Other Charge" runat="server"></asp:Label>&nbsp;
                                                            </td>
                                                            <td width="150">
                                                                <asp:TextBox runat="server" ID="txtOtherCharge" CssClass="x-large" placeholder="Other Charge"
                                                                    Text="0" Visible="false"></asp:TextBox>
                                                            </td>
                                                            <td width="60" class="innerfield_title">
                                                                <asp:Label ID="Label29" Text="Adj. Amt" runat="server" Visible="false"></asp:Label>
                                                            </td>
                                                            <td width="150">
                                                                <asp:TextBox runat="server" ID="txtAdj" CssClass="x-large" placeholder="Adj. Amt"
                                                                    Text="0" Visible="false"></asp:TextBox>
                                                            </td>
                                                            <td width="90" class="innerfield_title">&nbsp;
                                                            </td>
                                                            <td>&nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 8px 10px">
                                                <table border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td width="80" class="innerfield_title">
                                                            <asp:Label ID="lblRemarks" Text="Remarks" runat="server"></asp:Label>
                                                        </td>
                                                        <td width="320">
                                                            <asp:TextBox ID="txtRemarks" runat="server" MaxLength="50" CssClass="mid" TextMode="MultiLine" Style="width: 300px; height: 80px"> </asp:TextBox>
                                                        </td>
                                                        <td width="15">&nbsp;
                                                        </td>
                                                        <td width="100" align="left" class="innerfield_title">
                                                            <asp:Label ID="lblCheckerNote" Text="Rejection Note" runat="server"></asp:Label>
                                                        </td>
                                                        <td width="310">
                                                            <asp:TextBox ID="txtCheckerNote" runat="server" TextMode="MultiLine" Enabled="false"
                                                                Style="width: 290px; height: 80px" MaxLength="255"> </asp:TextBox>
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                        <tr>
                                                            <td height="8" colspan="6"></td>
                                                        </tr>
                                                    <tr>
                                                        <td align="left">&nbsp;
                                                        </td>
                                                        <td colspan="5">
                                                            <div class="btn_24_blue" id="divsave" runat="server">
                                                                <span class="icon disk_co"></span>
                                                                <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="btn_link" OnClick="btnSubmit_Click"
                                                                    ValidationGroup="Save" />
                                                            </div>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                                            <div class="btn_24_blue" id="divbtnCancel" runat="server">
                                                                <span class="icon cross_octagon_co"></span>
                                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn_link" CausesValidation="false"
                                                                    OnClick="btnCancel_Click" />
                                                            </div>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                                            <div class="btn_24_green" id="divbtnapprove" runat="server">
                                                                <span class="icon approve_co"></span>
                                                                <asp:LinkButton ID="btnApprove" runat="server" Text="Approve" CssClass="btn_link" ValidationGroup="approval"
                                                                    CausesValidation="false" OnClick="btnApprove_Click" />
                                                            </div>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                                            <div class="btn_24_red" id="divbtnreject" runat="server">
                                                                <span class="icon reject_co"></span>
                                                                <asp:LinkButton ID="btnReject" runat="server" Text="Reject" CssClass="btn_link" CausesValidation="false"
                                                                    OnClick="btnReject_Click" />
                                                            </div>
                                                            <asp:HiddenField runat="server" ID="hdnDespatchID" />
                                                            <asp:HiddenField runat="server" ID="hdnWaybillNo" />
                                                            <asp:HiddenField runat="server" ID="hdnCustomerID" />
                                                            <asp:HiddenField runat="server" ID="Hdn_Print" />
                                                        </td>
                                                    </tr>
                                                </table>
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
                                                        <td valign="top" style="padding-top: 10px;" width="90">
                                                            <asp:Label ID="Label16" runat="server" Text="From Date"></asp:Label>
                                                        </td>
                                                        <td width="165">
                                                            <asp:TextBox ID="txtFromDate" runat="server" MaxLength="10" Width="80" Enabled="false"
                                                                placeholder="dd/MM/yyyy" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="Search"
                                                                Font-Bold="true"></asp:TextBox>
                                                            <asp:ImageButton ID="imgPopup" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarFromDate" PopupButtonID="imgPopup" runat="server"
                                                                TargetControlID="txtFromDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>
                                                        <td valign="top" style="padding-top: 10px;" width="70">
                                                            <asp:Label ID="Label19" runat="server" Text="To Date"></asp:Label>

                                                        </td>
                                                        <td width="165">
                                                            <asp:TextBox ID="txtToDate" runat="server" MaxLength="10" Width="80" Enabled="false"
                                                                placeholder="dd/mm/yyyy" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="Search"
                                                                Font-Bold="true"></asp:TextBox>
                                                            <asp:ImageButton ID="imgPopuptodate" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtenderToDate" PopupButtonID="imgPopuptodate"
                                                                runat="server" TargetControlID="txtToDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>
                                                        <td valign="top" width="110">
                                                            <div class="btn_24_blue">
                                                                <span class="icon find_co"></span>
                                                                <asp:Button ID="btnSearchInvoice" runat="server" Text="Search" CssClass="btn_link"
                                                                    ValidationGroup="Search" OnClick="btnSearchInvoice_Click" />
                                                            </div>
                                                        </td>
                                                        <td width="60" valign="top" style="padding-top: 10px;">
                                                            <asp:Label ID="Label120" runat="server" Text="Filter" Visible="false"></asp:Label>
                                                        </td>
                                                        <td valign="top">
                                                            <asp:DropDownList ID="ddlWaybillFilter" Width="150" runat="server" class="chosen-select" Visible="false"
                                                                data-placeholder="Choose Waybill Filter Mode" AutoPostBack="true">
                                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="Without Waybill" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="With Waybill" Value="2"></asp:ListItem>
                                                            </asp:DropDownList>
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
                                    <div class="gridcontent-inner" style="padding-left: 8px;">
                                        <cc1:Grid ID="grdDespatchHeader" runat="server" CallbackMode="false" Serialize="true"
                                            FolderStyle="../GridStyles/premiere_blue" AutoGenerateColumns="false" OnDeleteCommand="DeleteRecordInvoice"
                                            AllowAddingRecords="false" AllowFiltering="true" AllowPageSizeSelection="false" OnRowDataBound="grdDespatchHeader_RowDataBound">
                                            <ClientSideEvents OnBeforeClientDelete="OnBeforeInvoiceDelete" OnClientDelete="OnDeleteInvoiceDetails" />
                                            <FilteringSettings InitialState="Visible" />
                                            <Columns>
                                                <cc1:Column ID="Column12" DataField="Sl" HeaderText="SL" runat="server" Width="60">
                                                    <TemplateSettings TemplateId="slnoTemplateIN" />
                                                </cc1:Column>
                                                <cc1:Column ID="Column19" DataField="SALERETURNID" HeaderText="PURCHASERETURNID" runat="server"
                                                    Width="60" Visible="false" />
                                                <cc1:Column ID="Column91" DataField="SALERETURNNO" HeaderText="PURCHASE-RET NO." runat="server"
                                                    Width="200">
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
                                                <cc1:Column ID="Column8" DataField="SALERETURNDATE" HeaderText="DATE" runat="server"
                                                    Width="100">
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
                                                <cc1:Column ID="Column89" DataField="DISTRIBUTORNAME" HeaderText="VENDOR" runat="server"
                                                    Width="205" Wrap="true">
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
                                                <cc1:Column ID="Column79" DataField="FINYEAR" HeaderText="FINYEAR" runat="server"
                                                    Width="132" />
                                                <cc1:Column ID="Column199" DataField="DISTRIBUTORID" HeaderText="VENDORID" runat="server"
                                                    Width="100" Visible="false" />
                                                <cc1:Column ID="Column25" DataField="ISVERIFIEDDESC" HeaderText="FINANCIAL STATUS"
                                                    runat="server" Width="146" Wrap="true">
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

                                                <cc1:Column ID="Column2" DataField="USERNAME" HeaderText="ENTRY USER"
                                                    runat="server" Width="100" Wrap="true">
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

                                                <cc1:Column ID="Column936" DataField="NETAMOUNT" HeaderText="NET AMOUNT" runat="server" Width="120" />
                                                <cc1:Column ID="Column129" AllowEdit="false" AllowDelete="false" HeaderText="Update Waybill"
                                                    runat="server" Visible="false" Width="70" Wrap="true">
                                                    <TemplateSettings TemplateId="editWaybillBtnTemplate" />
                                                </cc1:Column>
                                                <cc1:Column ID="Column92" AllowEdit="true" AllowDelete="true" HeaderText="VIEW" runat="server"
                                                    Width="60">
                                                    <TemplateSettings TemplateId="editBtnTemplate" />
                                                </cc1:Column>
                                                <cc1:Column ID="Column1" AllowEdit="false" AllowDelete="false" HeaderText="PRINT"
                                                    runat="server" Width="60" Wrap="true">
                                                    <TemplateSettings TemplateId="PrintBtnTemplate" />
                                                </cc1:Column>
                                                <cc1:Column HeaderText="CANCEL" AllowEdit="false" AllowDelete="true" Width="95">
                                                    <TemplateSettings TemplateId="deleteBtnTemplateInvoice" />
                                                </cc1:Column>
                                            </Columns>
                                            <FilteringSettings MatchingType="AnyFilter" />
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="editBtnTemplate">
                                                    <Template>
                                                        <a href="javascript: //" class="h_icon zoom_sl" id="btnGridEdit_<%# Container.PageRecordIndex %>"
                                                            onclick="CallServerMethod(this)"></a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="editWaybillBtnTemplate">
                                                    <Template>
                                                        <a href="javascript: //" class="filter_btn clipboard_text_co" id="btnGridWaybill_<%# Container.PageRecordIndex %>"
                                                            onclick="CallServerMethodWaybill(this)"></a>
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
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="deleteBtnTemplateInvoice">
                                                    <Template>
                                                        <a href="javascript: //" class="action-icons c-delete" title="Delete" onclick="grdDespatchHeader.delete_record(this)"></a>
                                                    </Template>
                                                </cc1:GridTemplate>
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
                                        <asp:Button ID="btnPrint" runat="server" Text="Print" Style="display: none" OnClick="btnPrint_Click"
                                            CausesValidation="false" />
                                    </div>
                                </asp:Panel>

                                <div id="lightRejectionNote" class="white_content" runat="server">
                                    <table width="50%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td width="10%" class="field_title">
                                                <asp:Label ID="Label17" runat="server" Text="Note"></asp:Label>&nbsp;<span class="req">*</span>
                                            </td>
                                            <td width="20%" class="field_input">
                                                <asp:TextBox ID="txtRejectionNote" runat="server" CssClass="x_large" TextMode="MultiLine"
                                                    Style="height: 119px" MaxLength="255"> </asp:TextBox>
                                                <asp:RequiredFieldValidator ID="CV_txtRejectionNote" runat="server" ControlToValidate="txtRejectionNote"
                                                    ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="RejectionNote"
                                                    ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title">&nbsp;
                                            </td>
                                            <td class="field_input">
                                                <asp:Button ID="btnRejectionNoteSubmit" runat="server" Text="Submit" CssClass="btn_small btn_blue"
                                                    ValidationGroup="RejectionNote" OnClick="btnRejectionNoteSubmit_Click" />&nbsp;&nbsp;<asp:Button
                                                        ID="btnRejectionCloseLightbox" runat="server" Text="Close" CssClass="btn_small btn_blue"
                                                        OnClick="btnRejectionCloseLightbox_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div id="fadeRejectionNote" class="black_overlay" runat="server">
                                </div>

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
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
